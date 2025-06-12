using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
namespace QuickMock.Helpers;

public static class HtmlHelpers
{
    public static IHtmlContent ValidatedTextBoxFor<TModel, TResult>(
        this IHtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TResult>> expression,
        object htmlAttributes = null)
    {
        var attributes = ValidationForBase(htmlHelper, expression, htmlAttributes);
        return htmlHelper.TextBoxFor(expression, attributes);
    }    
    
    public static IHtmlContent ValidatedTextAreaFor<TModel, TResult>(
        this IHtmlHelper<TModel> htmlHelper,
        Expression<Func<TModel, TResult>> expression,
        object htmlAttributes = null)
    {
        var attributes = ValidationForBase(htmlHelper, expression, htmlAttributes);
        return htmlHelper.TextAreaFor(expression, attributes);
    }

    private static IDictionary<string, object> ValidationForBase<TModel, TResult>(IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression,
        object htmlAttributes)
    {
        var fieldName = htmlHelper.NameFor(expression);
        var validationClass = htmlHelper.GetValidationClass(fieldName);
        var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
        
        if (attributes.ContainsKey("class"))
            attributes["class"] += " " + validationClass;
        else
            attributes["class"] = "form-control " + validationClass;
        return attributes;
    }
    
    private static string GetValidationClass(this IHtmlHelper htmlHelper, string fieldName)
    {
        var viewData = htmlHelper.ViewData;
        return viewData.ModelState[fieldName]?.Errors?.Count > 0 ? "is-invalid" : "";
    }
}