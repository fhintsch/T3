using Dp2Lib.Models;
using MvvmHelpers;

namespace T3.ViewModels
{
    public partial class SearchPart : BaseViewModel
    {
        public Func<string, List<Reference>> SearchFunction { get; set; }
    }

    public partial class SearchViewModel : BaseViewModel
    {
        private string searchPattern;

        private List<ReferenceGroup> items;

        private List<SearchPart> SearchParts { get; }
        public ObservableRangeCollection<ReferenceGroup> ResultSet { get; } = new();

        public SearchViewModel()
        {
            var searchList = new List<SearchPart>();
            searchList.Add(new SearchPart
            {
                Title = "Wartungsformulare",
                SearchFunction = SearchMaintenanceForms,
            });
            searchList.Add(new SearchPart
            {
                Title = "Artikel",
                SearchFunction = SearchComponentTypes
            });
            searchList.Add(new SearchPart
            {
                Title = "Dokumente",
                SearchFunction = SearchDocuments
            });
            SearchParts = searchList;
        }

        private class MaintenanceForm
        {
            internal string Title;
            internal string Description;
        }

        private List<Reference> SearchMaintenanceForms(string pattern)
        {
            var maintenanceFormList = new List<MaintenanceForm>();
            maintenanceFormList.Add(new MaintenanceForm { Title = "Wartung 1", Description = "Beschreibung 1" });
            maintenanceFormList.Add(new MaintenanceForm { Title = "Wartung 2", Description = "Beschreibung 2" });
            try
            {
                var resultSet = new List<Reference>();
                var dbresult = maintenanceFormList;
                var filteredresult = dbresult.Where(x => ((x.Title?.ToLower().Contains(pattern)) ?? false)
                    || ((x.Description?.ToLower().Contains(pattern)) ?? false)).ToList<MaintenanceForm>();
                foreach (var item in filteredresult)
                {
                    resultSet.Add(new Reference
                    {
                        Ident = item.Title,
                        Referer = item,
                    });
                }
                return resultSet;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}");
                return new List<Reference>();
            }
        }


        private class ComponentType
        {
            internal string Model;
            internal string Manufacturer;
            internal string Description;
        }
        private List<Reference> SearchComponentTypes(string pattern)
        {
            var componentTypeList = new List<ComponentType>();
            componentTypeList.Add(new ComponentType { Model = "COMPACT 3010", Manufacturer = "Alde", Description = "Alde Compact 3020HE Warmwasserheizung" });
            componentTypeList.Add(new ComponentType { Model = "LB 50", Manufacturer = "Büttner", Description = "Büttner Ladebooster LB 50" });
            var resultSet = new List<Reference>();
            var dbresult = componentTypeList;
            var filteredresult = dbresult.Where(x => ((x.Model?.ToLower().Contains(pattern)) ?? false)
                || ((x.Description?.ToLower().Contains(pattern)) ?? false)
                || ((x.Manufacturer?.ToLower().Contains(pattern)) ?? false)).ToList<ComponentType>();
            foreach (var item in filteredresult)
            {
                resultSet.Add(new Reference
                {
                    Ident = item.Model,
                    Referer = item,
                });
            }
            return resultSet;
        }

        private class Document
        {
            internal string Description;
        }
        private List<Reference> SearchDocuments(string pattern)
        {
            var documentList = new List<Document>();
            documentList.Add(new Document { Description = "Schönes Bild" });
            documentList.Add(new Document { Description = "Normales Bild" });
            var resultSet = new List<Reference>();
            var dbresult = documentList;
            var filteredresult = dbresult.Where(x => x.Description?.ToLower().Contains(pattern) ?? false).ToList<Document>();
            foreach (var item in filteredresult)
            {
                resultSet.Add(new Reference
                {
                    Ident = item.Description,
                    Referer = item,
                });
            }
            return resultSet;
        }

        public string SearchPattern
        {
            get => searchPattern;
            set
            {
                if (SetProperty(ref searchPattern, value))
                {
                    if (searchPattern.Length > 0)
                    {
                        items = new List<ReferenceGroup>();
                        DoSearch(searchPattern);
                        ResultSet.ReplaceRange(items);
                    }
                    else
                    {
                        ResultSet.ReplaceRange(new List<ReferenceGroup>());
                    }
                    Console.WriteLine($"++ {ResultSet.Count} Items");
                }
            }
        }

        internal void DoSearch(string pattern)
        {
            var normalizedPattern = pattern.ToLower();
            foreach (var part in SearchParts)
            {
                Console.WriteLine($"=={part.Title}==");
                var result = part.SearchFunction(normalizedPattern);
                Console.WriteLine($"=={result.Count}==");
                if (result.Count > 0)
                {
                    var group = new ReferenceGroup(part.Title, result);
                    items.Add(group);
#if true
                    Console.WriteLine($"--{part.Title}--");
                    foreach (var item in group)
                    {
                        Console.Write($" '{item.Ident}'");
                    }
                    Console.WriteLine("--");
#endif
                }
            }
        }
    }
}

