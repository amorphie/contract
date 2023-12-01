using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.core.Model.Document;

namespace amorphie.contract.core.Extension
{
    public static class ModelHelper
    {
        private static void FillNamesBasedOnLanguageInList<T>(List<T>? list, string language) where T : IMultilanguageTextHolder
        {
            if (list == null || list.Count == 0)
            {
                return;
            }

            foreach (var item in list)
            {
                var selectedLanguageText = item.MultilanguageText
                    .FirstOrDefault(t => t.Language == language);

                if (selectedLanguageText != null)
                {
                    item.Name = selectedLanguageText.Label;
                }
                else if (item.MultilanguageText.Any())
                {
                    item.Name = item.MultilanguageText.First().Label;
                }
            }
        }
    }
}