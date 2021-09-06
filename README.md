[![Build](https://img.shields.io/appveyor/ci/olegsych/chronology/master)](https://ci.appveyor.com/project/olegsych/chronology/branch/master)
[![Tests](https://img.shields.io/appveyor/tests/olegsych/chronology/master)](https://ci.appveyor.com/project/olegsych/chronology/branch/master/tests)
[![Nuget](https://img.shields.io/nuget/v/chronology.svg)](https://www.nuget.org/packages/chronology)

Chronology is a simple .NET API for applications that need to use UTC time.

# install

Add the [chronology](https://www.nuget.org/packages/chronology) package to your .NET project.
```PowerShell
dotnet add package chronology
```

# import

Import the `Chronology` namespace in your .NET source file.
```C#
using Chronology;
```

# use

Inject the `IClock` interface into your class.
```C#
public class YourClass
{
    readonly IClock<UtcDateTime> clock;
    public YourClass(IClock<UtcDateTime> clock) => this.clock = clock;
}
```

`IClock<UtcDateTime>` allows you to obtain the current date and time as `UtcDateTime`.
```C#
public override string ToString() {
    UtcDateTime now = clock.Now;
    return $"Current time is {now}";
}
```

In your application code, instantiate `UniversalClock` and pass it to your class
as the implementation of the `IClock<UtcDateTime>` interface. If you are using
dependency injection, register `UniversalClock` as a singleton implementation
of the `IClock<UtcDateTime>` service.
```C#
public static void Main() {
    var clock = new UniversalClock();
    var work = new YourClass(clock);
    Console.WriteLine(work);
}
```

In your test code, you can provide a stub implementation of `IClock<UtcDateTime>`
that returns a fixed value and test your code deterministically.
```C#
using NSubstitute;
using Xunit;

[Fact]
public void YourTest() {
    var clock = Substitute.For<IClock<UtcDateTime>>();
    var now = new DateTime(2021, 9, 4, 12, 00, 00, DateTimeKind.Utc);
    ConfiguredCall? arrange = clock.Now.Returns(new UtcDateTime(now));

    var sut = new YourClass(clock);
    string actual = sut.ToString();

    string expected = $"Current time is {now:o}";
    Assert.Equal(expected, actual);
}
```

`UtcDateTime` is a strongly-typed wrapper of `DateTime` with `DateTimeKind.Utc`.
It can be implicitly converted to `DateTime` or `DateTimeOffset` and implements
similar properties, methods and operators.
