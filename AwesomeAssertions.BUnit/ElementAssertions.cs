using System.Text.RegularExpressions;
using AngleSharp.Dom;
using Bunit;
using AwesomeAssertions;
using AwesomeAssertions.Execution;
using AwesomeAssertions.Primitives;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;

namespace AwesomeAssertions.BUnit;

/// <summary>
/// Contains a number of methods to assert that an <see cref="IElement"/> is in the expected state.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="RenderedFragmentAssertions"/> class.
/// </remarks>
public class ElementAssertions(IElement value) : ElementAssertions<ElementAssertions>(value)
{
}

public class ElementAssertions<TAssertions>(IElement value) : ReferenceTypeAssertions<IElement, TAssertions>(value, AssertionChain.GetOrCreate()) 
    where TAssertions : ElementAssertions<TAssertions>
{
    private readonly BunitContext _testContext = new BunitContext();

    /// <summary>
    /// Returns the type of the subject the assertion applies on.
    /// </summary>
    protected override string Identifier => "IElement";

    public AndConstraint<TAssertions> HaveChildMarkup(RenderFragment expected, string because = "", params object[] becauseArgs)
    {
        var child = Subject.FirstChild;
        
        CurrentAssertionChain
            .BecauseOf(because, becauseArgs)
            .ForCondition(child is not null)
            .WithDefaultIdentifier(Identifier)
            .FailWith("Expected {context} to have child {0}{reason}, but found <null>.", _testContext.Render(expected).Markup);
    
        if (CurrentAssertionChain.Succeeded)
        {
            bool doesMarkupMatch;
    
            try
            {
                child!.MarkupMatches(expected);
                doesMarkupMatch = true;
            }
            catch
            {
                doesMarkupMatch = false;
            }
            
            CurrentAssertionChain
                .BecauseOf(because, becauseArgs)
                .ForCondition(doesMarkupMatch)
                .WithDefaultIdentifier(Identifier)
                .FailWith("Expected {context} to have child {0}{reason}, but found {1}.", _testContext.Render(expected).Markup, child is IElement childElement ? Regex.Replace(childElement.OuterHtml ?? "", @">\s+<", "><").Trim() : child?.ToString() ?? "<null>");
        }
    
        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    
    public AndConstraint<TAssertions> HaveChildMarkup(string expected, string because = "", params object[] becauseArgs)
    {
        var child = Subject.FirstChild;
        
        CurrentAssertionChain
            .BecauseOf(because, becauseArgs)
            .ForCondition(child is not null)
            .WithDefaultIdentifier(Identifier)
            .FailWith("Expected {context} to have child {0}{reason}, but found <null>.", expected);
    
        if (CurrentAssertionChain.Succeeded)
        {
            bool doesMarkupMatch;
    
            try
            {
                child!.MarkupMatches(expected);
                doesMarkupMatch = true;
            }
            catch
            {
                doesMarkupMatch = false;
            }
            
            CurrentAssertionChain
                .BecauseOf(because, becauseArgs)
                .ForCondition(doesMarkupMatch)
                .WithDefaultIdentifier(Identifier)
                .FailWith("Expected {context} to have child {0}{reason}, but found {1}.", expected, child is IElement childElement ? Regex.Replace(childElement.OuterHtml ?? "", @">\s+<", "><").Trim() : child?.ToString() ?? "<null>");
        }
    
        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    
    public AndConstraint<TAssertions> HaveMarkup(RenderFragment expected, string because = "", params object[] becauseArgs)
    {
        bool doesMarkupMatch;
    
        try
        {
            Subject.MarkupMatches(expected);
            doesMarkupMatch = true;
        }
        catch
        {
            doesMarkupMatch = false;
        }
        
        CurrentAssertionChain
            .BecauseOf(because, becauseArgs)
            .ForCondition(doesMarkupMatch)
            .WithDefaultIdentifier(Identifier)
            .FailWith("Expected {context} markup {0}{reason}, but found {1}.", _testContext.Render(expected).Markup, Regex.Replace(Subject.OuterHtml, @">\s+<", "><").Trim());
            return new AndConstraint<TAssertions>((TAssertions)this);
    }
    
    public AndConstraint<TAssertions> HaveMarkup(string expected, string because = "", params object[] becauseArgs)
    {
        bool doesMarkupMatch;
    
        try
        {
            Subject.MarkupMatches(expected);
            doesMarkupMatch = true;
        }
        catch
        {
            doesMarkupMatch = false;
        }
        
        CurrentAssertionChain
            .BecauseOf(because, becauseArgs)
            .ForCondition(doesMarkupMatch)
            .WithDefaultIdentifier(Identifier)
            .FailWith("Expected {context} markup {0}{reason}, but found {1}.", expected, Regex.Replace(Subject.OuterHtml, @">\s+<", "><").Trim());
    
        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    
    public AndConstraint<TAssertions> NotHaveClass(string expected, string because = "", params object[] becauseArgs)
    {
        CurrentAssertionChain
            .BecauseOf(because, becauseArgs)
            .WithDefaultIdentifier("Subject.ClassList")
            .ForCondition(!Subject.ClassList.Contains(expected))
            .FailWith("Expected {context} {0} to not contain {1}{reason}.", Subject.ClassList, expected);
        
        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    
    public AndConstraint<TAssertions> HaveClass(string expected, string because = "", params object[] becauseArgs)
    {
        CurrentAssertionChain
            .BecauseOf(because, becauseArgs)
            .WithDefaultIdentifier("Subject.ClassList")
            .ForCondition(Subject.ClassList.Contains(expected))
            .FailWith("Expected {context} {0} to contain {1}{reason}.", Subject.ClassList, expected);
        
        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    
    public AndConstraint<TAssertions> HaveRel(string expected, string because = "", params object[] becauseArgs)
    {
        var attribute = Subject.Attributes["rel"];
        
        CurrentAssertionChain
            .BecauseOf(because, becauseArgs)
            .ForCondition(attribute is not null)
            .WithDefaultIdentifier(Identifier)
            .FailWith("Expected {context} to have attribute {0}{reason}, but found <null>.", "rel");
    
        if (CurrentAssertionChain.Succeeded)
        {
            var collection = attribute!.Value.Split(" ").ToList();
            CurrentAssertionChain
                .BecauseOf(because, becauseArgs)
                .ForCondition(attribute!.Value.Split(" ").Contains(expected))
                .WithDefaultIdentifier(Identifier)
                .FailWith("Expected {context} {0} [{1}] to contain {2}{reason}.", "rel", collection, expected);
        }
    
        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    
    public AndConstraint<TAssertions> HaveTag(string expected, string because = "", params object[] becauseArgs)
    {
        var tag = Subject.LocalName;
        
        CurrentAssertionChain
            .BecauseOf(because, becauseArgs)
            .ForCondition(tag.Equals(expected, StringComparison.InvariantCulture))
            .WithDefaultIdentifier(Identifier)
            .FailWith("Expected {context} {0} to be {1}{reason}, but found {2}.", "tag", expected, tag);
        
        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    
    public AndConstraint<TAssertions> HaveAltText(string expected, string because = "", params object[] becauseArgs) 
        => HaveAttribute("alt", expected, because, becauseArgs);
    
    public AndConstraint<TAssertions> HaveAriaLabel(string expected, string because = "", params object[] becauseArgs) 
        => HaveAttribute("aria-label", expected, because, becauseArgs);
    
    public AndConstraint<TAssertions> HaveDataTestClass(string expected, string because = "", params object[] becauseArgs) 
        => HaveAttribute("data-test-class", expected, because, becauseArgs);
    
    public AndConstraint<TAssertions> HaveDataTestId(string expected, string because = "", params object[] becauseArgs) 
        => HaveAttribute("data-test-id", expected, because, becauseArgs);
    
    public AndConstraint<TAssertions> HaveHref(string expected, string because = "", params object[] becauseArgs) 
        => HaveAttribute("href", expected, because, becauseArgs);
    
    public AndConstraint<TAssertions> HaveId(string expected, string because = "", params object[] becauseArgs) 
        => HaveAttribute("id", expected, because, becauseArgs);
    
    public AndConstraint<TAssertions> HaveSrc(string expected, string because = "", params object[] becauseArgs) 
        => HaveAttribute("src", expected, because, becauseArgs);
    
    public AndConstraint<TAssertions> HaveTarget(string expected, string because = "", params object[] becauseArgs) 
        => HaveAttribute("target", expected, because, becauseArgs);
    
    public AndConstraint<TAssertions> HaveTitle(string expected, string because = "", params object[] becauseArgs) 
        => HaveAttribute("title", expected, because, becauseArgs);
    
    public AndConstraint<TAssertions> HaveType(string expected, string because = "", params object[] becauseArgs) 
        => HaveAttribute("type", expected, because, becauseArgs);
    
    public AndConstraint<TAssertions> HaveAttribute(string attributeName, string expectedValue, string because = "", params object[] becauseArgs)
    {
        var attribute = Subject.Attributes[attributeName];
        
        CurrentAssertionChain
            .BecauseOf(because, becauseArgs)
            .ForCondition(attribute is not null)
            .WithDefaultIdentifier(Identifier)
            .FailWith("Expected {context} to have attribute {0}{reason}, but found <null>.", attributeName);
        
        if (CurrentAssertionChain.Succeeded)
        {
            CurrentAssertionChain
                .BecauseOf(because, becauseArgs)
                .ForCondition(attribute!.Value == expectedValue)
                .WithDefaultIdentifier(Identifier)
                .FailWith("Expected {context} {0} attribute to have value {1}{reason}" +
                          ", but found {2}.", attributeName, expectedValue, attribute.Value);
        }
    
        return new AndConstraint<TAssertions>((TAssertions)this);
    }
}
