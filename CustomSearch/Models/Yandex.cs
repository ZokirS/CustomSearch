﻿

using System.ComponentModel.DataAnnotations;

namespace CustomSearch.Models
{
    public class Yandex
    {

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Snippet { get; set; }
    }
}
