﻿// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Calc.Interfaces;
using Enigma.Domain.RequestResponse;

namespace Enigma.Api.Astron;


/// <inheritdoc/>
public class CoordinateConversionApi : ICoordinateConversionApi
{
    private readonly ICoordinateConversionHandler _coordConvHandler;


    /// <param name="coordConvHandler">Handler for the conversion of coordinates.</param>
    public CoordinateConversionApi(ICoordinateConversionHandler coordConvHandler)
    {
        _coordConvHandler = coordConvHandler;
    }

    public CoordinateConversionResponse GetEquatorialFromEcliptic(CoordinateConversionRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.EclCoord);
        return _coordConvHandler.HandleConversion(request);
    }

}