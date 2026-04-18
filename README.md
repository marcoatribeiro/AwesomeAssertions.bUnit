[![MIT License](https://img.shields.io/github/license/dotnet/aspnetcore?color=%230b0&style=flat-square)](https://github.com/user/AwesomeAssertions.bUnit/blob/main/LICENSE)
[![Nuget](https://img.shields.io/nuget/v/TorinoInfo.AwesomeAssertions.BUnit)](https://www.nuget.org/packages/TorinoInfo.AwesomeAssertions.BUnit)

# AwesomeAssertions.bUnit

AwesomeAssertions.bUnit is an assertions library that is used in conjunction with bUnit.

It offers awesome assertions syntax to rendered components.

This project is based on [fluentassertions.bUnit](https://github.com/srpeirce/fluentassertions.bUnit) by Steve Peirce, adapted to use [AwesomeAssertions](https://github.com/AwesomeAssertions/AwesomeAssertions) instead of FluentAssertions.

## Why?

- Great for TDD
- Great for writing non-brittle tests
- Uses AwesomeAssertions (Apache 2.0 licensed community fork of FluentAssertions)


## Getting Started

Install Nuget Package into test project:
```
dotnet add package AwesomeAssertions.BUnit
```

Use bUnit to render component.

Then write assertions...

```csharp
    [Fact]
    public void RenderWithChildContent()
    {
        Render(@<Button><h1>Test</h1></Button>)
            .Should().HaveTag("button")
            .And.HaveChildMarkup(@<h1>Test</h1>);
    }

    [Fact]
    public void SetDefaultCssClasses()
    {
        Render(@<Button><h1>Test</h1></Button>)
            .Should().HaveClass("btn")
            .And.HaveClass("btn-primary")
            .And.NotHaveClass("btn-outline-success");
    }
```

## Documentation

Use intellisense and look at the code for now :-)

### Find Element

[Find Element](https://github.com/user/AwesomeAssertions.bUnit/blob/main/AwesomeAssertions.BUnit/BUnitExtensions.cs)

### Assertions

[Assertions](https://github.com/user/AwesomeAssertions.bUnit/blob/main/AwesomeAssertions.BUnit/ElementAssertions.cs)

## Contributions

Currently in a very early version/stage of this project.

Please raise issues and feel free to submit PRs (happy to discuss in an issue first to avoid wasted effort).

## Credits

Based on [fluentassertions.bUnit](https://github.com/srpeirce/fluentassertions.bUnit) by Steve Peirce, licensed under MIT.
