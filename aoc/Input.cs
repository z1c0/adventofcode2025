using System.Collections.Immutable;

namespace aoc
{
	public static partial class Input
	{
		public const string DEFAULT_INPUT_FILENAME = "input.txt";
		public const string DEFAULT_SAMPLE_INPUT_FILENAME = "sample.txt";
		
		//
		// Read... from file
		//
		public static ImmutableList<char> ReadCharList(string fileName = DEFAULT_INPUT_FILENAME) => [.. File.ReadAllText(fileName)];
		public static ImmutableList<string> ReadStringList(string fileName = DEFAULT_INPUT_FILENAME) => [.. File.ReadAllLines(fileName)];
		public static ImmutableList<int> ReadIntList(string fileName = DEFAULT_INPUT_FILENAME) => [.. ReadStringList(fileName).Select(int.Parse)];
		public static ImmutableList<ImmutableList<int>> ReadIntListList(string fileName = DEFAULT_INPUT_FILENAME) =>
			[.. ReadStringList(fileName).Select(s => s.Split(' ').Select(int.Parse).ToImmutableList()).ToImmutableList()];
		public static ImmutableList<(string s1, string s2)> ReadStringStringList(string fileName = DEFAULT_INPUT_FILENAME) =>
			ReadTokenized(fileName, tokens => (tokens[0], tokens[1]));		
		public static ImmutableList<(char c, int i)> ReadCharIntList(string fileName = DEFAULT_INPUT_FILENAME) =>
			ReadTokenized(fileName, tokens => (tokens[0][0], int.Parse(tokens[1])));		
		public static IEnumerable<(string s, int i)> ReadStringIntList(string fileName = DEFAULT_INPUT_FILENAME) =>
			ReadTokenized(fileName, tokens => (tokens[0], int.Parse(tokens[1])));
		public static ImmutableList<(int i1, int i2)> ReadIntIntList(string fileName = DEFAULT_INPUT_FILENAME) =>
			ParseIntIntList(ReadStringList(fileName), ",");
		public static ImmutableList<(int i1, int i2, int i3)> ReadIntIntIntList(string fileName = DEFAULT_INPUT_FILENAME) =>
			ParseIntIntIntList(ReadStringList(fileName), ",");
		public static ImmutableList<(long l1, long l2)> ReadLongLongList(string fileName = DEFAULT_INPUT_FILENAME) =>
			ParseLongLongList(ReadStringList(fileName), ",");
		public static ImmutableList<(long l1, long l2, long l3)> ReadLongLongLongList(string fileName = DEFAULT_INPUT_FILENAME) =>
			ParseLongLongLongList(ReadStringList(fileName), ",");
		public static (ImmutableList<string> block1, ImmutableList<string> block2) ReadBlocks(string fileName = DEFAULT_INPUT_FILENAME)
		{
			var lines = ReadStringList(fileName);
			var split = lines.FindIndex(string.IsNullOrWhiteSpace);
			return (lines.GetRange(0, split), lines.GetRange(split + 1, lines.Count - split - 1));
		}

		//
		// Parse ... from string
		//
		public static ImmutableList<string> ParseStringList(string text, string delimiter = " ") => [.. text.Split(delimiter, StringSplitOptions.RemoveEmptyEntries)];
		public static ImmutableList<int> ParseIntList(string text, string delimiter = " ") => ParseIntList(ParseStringList(text, delimiter));
		public static ImmutableList<int> ParseIntList(IEnumerable<string> tokens) => [.. tokens.Select(int.Parse)];
		public static ImmutableList<long> ParseLongList(string text, string delimiter = " ") => ParseLongList(ParseStringList(text, delimiter));
		public static ImmutableList<long> ParseLongList(IEnumerable<string> tokens) => [.. tokens.Select(long.Parse)];
		public static ImmutableList<(int i1, int i2)> ParseIntIntList(string text, string stringDelimiter = " ", string intDelimiter = " ") =>
			ParseIntIntList(ParseStringList(text, stringDelimiter), intDelimiter);
		public static ImmutableList<(int i1, int i2)> ParseIntIntList(IEnumerable<string> tokens, string intDelimiter = " ") =>
			ParseTokenized(tokens, tokens => (int.Parse(tokens[0]), int.Parse(tokens[1])), intDelimiter);
		public static ImmutableList<(int i1, int i2, int i3)> ParseIntIntIntList(string text, string stringDelimiter = " ", string intDelimiter = " ") =>
			ParseIntIntIntList(ParseStringList(text, stringDelimiter), intDelimiter);
		public static ImmutableList<(int i1, int i2, int i3)> ParseIntIntIntList(IEnumerable<string> tokens, string intDelimiter = " ") =>
			ParseTokenized(tokens, tokens => (int.Parse(tokens[0]), int.Parse(tokens[1]), int.Parse(tokens[2])), intDelimiter);
		public static ImmutableList<(long l1, long l2)> ParseLongLongList(string text, string stringDelimiter = " ", string longDelimiter = " ") =>
			ParseLongLongList(ParseStringList(text, stringDelimiter), longDelimiter);
		public static ImmutableList<(long l1, long l2)> ParseLongLongList(IEnumerable<string> tokens, string longDelimiter = " ") =>
			ParseTokenized(tokens, tokens => (long.Parse(tokens[0]), long.Parse(tokens[1])), longDelimiter);
		public static ImmutableList<(long l1, long l2, long l3)> ParseLongLongLongList(string text, string stringDelimiter = " ", string longDelimiter = " ") =>
			ParseLongLongLongList(ParseStringList(text, stringDelimiter), longDelimiter);
		public static ImmutableList<(long l1, long l2, long l3)> ParseLongLongLongList(IEnumerable<string> tokens, string longDelimiter = " ") =>
			ParseTokenized(tokens, tokens => (long.Parse(tokens[0]), long.Parse(tokens[1]), long.Parse(tokens[2])), longDelimiter);

		//
		// Helpers
		//		
		private static ImmutableList<T> ReadTokenized<T>(string fileName, Func<string[], T> tokenParser) =>
			[.. ParseTokenized(ReadStringList(fileName), tokenParser, " ")];
		private static ImmutableList<T> ParseTokenized<T>(IEnumerable<string> tokens, Func<string[], T> tokenParser, string delimiter) =>
			[.. tokens.Select(l => tokenParser(l.Split(delimiter, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)))];
	}
}
