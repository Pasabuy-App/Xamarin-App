using Xamarin.Forms.Internals;

namespace PasaBuy.App.Controls
{
    [Preserve(AllMembers = true)]
    public class SearchableFood : SearchableListView
    {
        public override bool FilterContacts(object obj)
        {
            if (base.FilterContacts(obj))
            {
                var taskInfo = obj as Models.Marketplace.FoodStore;

                if (taskInfo == null || string.IsNullOrEmpty(taskInfo.Title) || string.IsNullOrEmpty(taskInfo.Description) || string.IsNullOrEmpty(taskInfo.Barangay) ||
                    string.IsNullOrEmpty(taskInfo.Province))
                {
                    return false;
                }

                return taskInfo.Title.ToUpperInvariant().Contains(this.SearchText.ToUpperInvariant())
                    || taskInfo.Logo.ToUpperInvariant().Contains(this.SearchText.ToUpperInvariant())
                    || taskInfo.Description.ToUpperInvariant().Contains(this.SearchText.ToUpperInvariant()) ||
                    taskInfo.Barangay.ToUpperInvariant().Contains(this.SearchText.ToUpperInvariant()) ||
                    taskInfo.Province.ToUpperInvariant().Contains(this.SearchText.ToUpperInvariant());

            }

            return false;

        }
    }
}
