﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Infrastructure.Constants
{
    public static class DataConstants
    {
        public const int GenreNameLenght = 50;
        public const int BookTitleMinLenght = 10;
        public const int BookTitleMaxLenght = 100;
        public const int BookAuthorMinLenght = 10;
        public const int BookAuthorMaxLenght = 50;
        public const int BookISBNMaxLenght = 13;
        public const int NumberMaxLenght = 13;
        public const int NumberMinLenght = 7;
        public const int CommentMaxLenght = 500;
        public const int CommentMinLenght = 10;
        public const int RatingMinValue = 1;
        public const int RatingMaxValue = 5;
		public const string DateFormat = "dd/MM/yyyy HH:mm";
	}
}
