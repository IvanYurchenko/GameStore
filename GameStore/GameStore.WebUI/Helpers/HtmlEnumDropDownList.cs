using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace GameStore.WebUI.Helpers
{
    public static class HtmlEnumDropDownList
    {
        private static readonly SelectListItem[] SingleEmptyItem = {new SelectListItem {Text = "", Value = ""}};

        public static MvcHtmlString EnumDropDownList<TEnum>(this HtmlHelper htmlHelper, string name, TEnum selectedValue)
        {
            IEnumerable<TEnum> values = Enum.GetValues(typeof (TEnum)).Cast<TEnum>();

            IEnumerable<SelectListItem> items = from value in values
                select new SelectListItem
                {
                    Text = value.ToString(),
                    Value = value.ToString(),
                    Selected = (value.Equals(selectedValue))
                };
            return htmlHelper.DropDownList(name, items);
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            Type enumType = GetNonNullableModelType(metadata);
            IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();

            IEnumerable<SelectListItem> items =
                values.Select(value => new SelectListItem
                {
                    Text = GetEnumDescription(value),
                    Value = GetValue(value).ToString(),
                    Selected = value.Equals(metadata.Model)
                });

            if (metadata.IsNullableValueType)
                items = SingleEmptyItem.Concat(items);

            return htmlHelper.DropDownListFor(
                expression,
                items,
                htmlAttributes
                );
        }

        public static List<SelectListItem> GetSelectedListForEnum<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TEnum>> expression, TEnum selectedValue, object htmlAttributes = null)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            Type enumType = GetNonNullableModelType(metadata);
            IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();

            var items = new List<SelectListItem>();
            foreach (var value in values)
            {
                var selectedListItem = new SelectListItem();
                selectedListItem.Text = GetEnumDescription(value);
                selectedListItem.Value = GetValue(value).ToString();
                selectedListItem.Selected =
                    (String.Equals(value.ToString(), selectedValue.ToString(), StringComparison.CurrentCultureIgnoreCase));

                items.Add(selectedListItem);
            }

            if (metadata.IsNullableValueType)
            {
                items = SingleEmptyItem.Concat(items).ToList();
            }
            return items;
        }

        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            Type realModelType = modelMetadata.ModelType;
            Type underlyingType = Nullable.GetUnderlyingType(realModelType);
            if (underlyingType != null)
                realModelType = underlyingType;

            return realModelType;
        }

        private static int GetValue<TEnum>(TEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            return (int) fi.GetValue(value);
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