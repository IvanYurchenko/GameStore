using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace GameStore.WebUI.Helpers
{
    public static class EnumRadioButtonList
    {
        public static MvcHtmlString EnumRadioButtonListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression)
        {
            return EnumRadioButtonListFor(htmlHelper, expression, null);
        }

        public static MvcHtmlString EnumRadioButtonListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            Type enumType = GetNonNullableModelType(metadata);
            IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();
            string fullName = ExpressionHelper.GetExpressionText(expression);

            IEnumerable<SelectListItem> items = from value in values
                select new SelectListItem
                {
                    Text = GetEnumDescription(value),
                    Value = value.ToString(),
                    Selected = value.Equals(metadata.Model)
                };

            var sb = new StringBuilder();

            foreach (var item in items)
            {
                // Generate an id to be given to the radio button field 
                var id = string.Format("rbl_{0}_{1}",
                    fullName.Replace("[", "").Replace("]", "").Replace(".", "_"),
                    item.Value);

                // Create and populate a radio button using the existing html helpers 
                var label = htmlHelper.Label(id, HttpUtility.HtmlEncode(item.Text));
                //var radio = htmlHelper.RadioButtonFor(expression, item.Value, new { id = id }).ToHtmlString();
                var radio = htmlHelper.RadioButton(fullName, item.Value, item.Selected, new {id}).ToHtmlString();
                sb.AppendFormat("{0} {1} {2}", radio, label, "<br/>");
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            Type realModelType = modelMetadata.ModelType;
            Type underlyingType = Nullable.GetUnderlyingType(realModelType);
            if (underlyingType != null)
                realModelType = underlyingType;

            return realModelType;
        }

        private static string GetEnumDescription<TEnum>(TEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[]) fi.GetCustomAttributes(typeof (DescriptionAttribute), false);

            if ((attributes != null) && (attributes.Length > 0))
                return attributes[0].Description;

            return value.ToString();
        }
    }
}