﻿using AutoMapper;
using Testnt.Common.Mappings;
using Testnt.Main.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
namespace Testnt.Main.Application.TestScenarios.List.Query
{
    public class GetTestScenarioListDto : IMapFrom<TestScenario>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TestScenario, GetTestScenarioListDto>();
        }
    }
}
