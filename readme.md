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
    readonly IClock clock;
    public YourClass(IClock clock) => this.clock = clock;
}
```

`IClock` allows you to obtain the current date and time as `UtcDateTime`
```C#
public override string ToString() {
    UtcDateTime now = clock.Time;
    return $"Current time is {now}";
}
```

In your application code, instantiate `Clock` and pass it to your class as the
implementation of the `IClock` interface. If you are using dependency injection,
register `Clock` as a singleton implementing the `IClock` service.
```C#
public static void Main() {
    var clock = new Clock();
    var work = new YourClass(clock);
    Console.WriteLine(work);
}
```

In your test code, you can provide a stub implementation of `IClock` that returns
a fixed value and test your code deterministically.
```C#
using NSubstitute;
using Xunit;

[Fact]
public void YourTest() {
    IClock clock = Substitute.For<IClock>();
    var time = new DateTime(2021, 9, 4, 12, 00, 00, DateTimeKind.Utc);
    ConfiguredCall? arrange = clock.Time.Returns(new UtcDateTime(time));

    var sut = new YourClass(clock);
    string actual = sut.ToString();

    string expected = $"Current time is {time:o}";
    Assert.Equal(expected, actual);
}
```

`UtcDateTime` is a strongly-typed wrapper of `DateTime` with `DateTimeKind.Utc`.
It can be implicitly converted to `DateTime` or `DateTimeOffset` and implements
similar properties, methods and operators.
