using EssentialLayers.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EssentialLayers.Helpers.Extension
{
	public static class OptionModelExtensions
	{
		public static ObservableCollection<OptionModel> ToOptionModel<T>(
			this IEnumerable<T> self,
			Func<T, string> descriptionSelector,
			Func<T, string> idSelector
		)
		{
			if (self == null) return [];

			return self.Select(
				x => new OptionModel(descriptionSelector(x), idSelector(x))
			).ToObservableCollection();
		}
	}
}