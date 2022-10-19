// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Engima.Research.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma.Research.Services;

public static class ResearchServices
{
    public static void RegisterResearchServices(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IControlGroupTypeSpecifications, ControlGroupTypeSpecifications>();
    }
}

