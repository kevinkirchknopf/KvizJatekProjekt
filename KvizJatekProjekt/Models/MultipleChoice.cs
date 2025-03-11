using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace LegyenOnIsMilliardosClassLibary.Models
{
    public class MultipleChoice
    {
        public int Id { get; set; }
        public string Kerdes_Szoveg { get; set; }
        public string? ImgSrc { get; set; }
        public string Jovalasz { get; set; }
        public string Valaszlehetosegek { get; set; } // Comma-separated answers

        [NotMapped]
        public List<string> ValaszlehetosegekList
        {
            get => Valaszlehetosegek?.Split(',').Select(s => s.Trim()).ToList() ?? new List<string>();
            set => Valaszlehetosegek = string.Join(",", value);
        }

        public void ScrambleAnswers()
        {
            var rnd = new Random();
            ValaszlehetosegekList = ValaszlehetosegekList.OrderBy(_ => rnd.Next()).ToList();
        }
    }
}
