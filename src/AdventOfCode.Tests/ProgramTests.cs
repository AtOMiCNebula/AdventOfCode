
using Microsoft.Extensions.DependencyInjection;

namespace NebulousIndustries.AdventOfCode.Tests;

public static class ProgramTests
{
    public static ServiceProvider GetTestServiceProvider<TDay>()
        where TDay : class, IDay
    {
        ServiceCollection collection = new();
        collection.AddTransient<TDay>();

        return collection.BuildServiceProvider();
    }
}
