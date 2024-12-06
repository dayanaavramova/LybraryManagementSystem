using LibraryManagementSystem.Core.Contracts;
using LibraryManagementSystem.Core.Services;
using LibraryManagementSystem.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Tests.UnitTests
{
    [TestFixture]
    public class MemberServiceTests : UnitTestsBase
    {
        private IMemberService memberService;

        [OneTimeSetUp]
        public void SetUp()
            => memberService = new MemberService(repository);

        [Test]
        public async Task GetMemberIdAsync_ShouldReturnCorrectUserid()
        {
            var resultId = memberService.GetMemberIdAsync(Member.UserId);
            Assert.That(Convert.ToInt32(resultId), Is.EqualTo(Member.Id));
        }

        [Test]
        public async Task ExistsByIdAsync_ShouldReturnTrue()
        {
            var result = await memberService.ExistsByIdAsync(Member.UserId);
            Assert.IsTrue(result);
        }


        [Test]
        public async Task UserWithPhoneNumberExistsAsync_ShouldReturnTrue()
        {
            var result = await memberService.UserWithPhoneNumberExistsAsync(Member.PhoneNumber);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task CreateAsync_ShouldWorkCorrectly()
        {
            var membersCountBefore = repository.AllReadOnly<Member>().Count();

            await memberService.CreateAsync(Member.UserId, Member.PhoneNumber);

            var membersCountAfter = repository.AllReadOnly<Member>().Count();
            Assert.That(membersCountAfter, Is.EqualTo(membersCountBefore + 1));

            var newMemberId = await memberService.GetMemberIdAsync(Member.UserId);
            var newMemberinDb = await repository.GetByIdAsync<Member>(newMemberId);
            Assert.IsNotNull(newMemberinDb);
            Assert.That(newMemberinDb.UserId, Is.EqualTo(Member.UserId));
            Assert.That(newMemberinDb.PhoneNumber, Is.EqualTo(Member.PhoneNumber));
        }

        [Test]
        public async Task UserHasRoleAsync_ShouldReturnFalse()
        {
            var result = await memberService.UserHasRoleAsync(Member.UserId);
            Assert.IsFalse(result);
        }
    }
}
