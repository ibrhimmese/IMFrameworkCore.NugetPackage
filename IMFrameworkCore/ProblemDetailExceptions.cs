﻿using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace IMFrameworkCore;

public static class ProblemDetailsExtensions
{
    public static string AsJson<TProblemDetail>(this TProblemDetail details)
        where TProblemDetail : ProblemDetails => JsonSerializer.Serialize(details);
}
