﻿using LanchesDev.Models;
using System.Collections.Generic;


namespace LanchesDev.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Lanche> LanchesPreferidos { get; set; }
    }
}
