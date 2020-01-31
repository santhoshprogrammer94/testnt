﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Testnt.Main.Application.Common;

namespace Testnt.Main.Application.TestScenarios.Query.List
{
    public class GetTestScenarioListQuery : BaseRequest, IRequest<GetObjectListVm<GetTestScenarioListDto>>
    {
    }
}