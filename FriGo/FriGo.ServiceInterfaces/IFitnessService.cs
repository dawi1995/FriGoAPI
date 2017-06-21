﻿using System;
using System.Collections.Generic;
using FriGo.Db.Models.Recipes;
using FriGo.Db.Models.Ingredients;

namespace FriGo.ServiceInterfaces
{
    public interface IFitnessService
    {
        IFitnessSearchEngine EngineFitness { get; set; }
    }
}