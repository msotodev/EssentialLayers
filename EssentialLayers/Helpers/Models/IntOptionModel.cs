namespace EssentialLayers.Helpers.Models
{
	public class IntOptionModel
	{
		public string Title { get; set; } = string.Empty;

		public int Value { get; set; }

		public IntOptionModel() { }

		public IntOptionModel(string title, int value)
		{
			Title = title;
			Value = value;
		}

		public static OptionModel Default => new(string.Empty, string.Empty);
	}
}
