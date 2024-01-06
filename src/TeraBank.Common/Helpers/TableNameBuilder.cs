namespace TeraBank.Common.Helpers;

public sealed class TableNameBuilder(Type type)
{
    private readonly Type _type = type;

    private const string S = "s";
    private const string ES = "es";
    private const string Y = "y";
    private const string I = "i";

    private readonly string[] _postfixes =
    [
        "s", "sh", "ch", "ss", "x", "z"
    ];

    private readonly char[] _vowels =
    [
        'a', 'i', 'e', 'o', 'u'
    ];

    public string Generate()
    {
        foreach (var postfix in _postfixes)
        {
            if (_type.Name.EndsWith(postfix, StringComparison.OrdinalIgnoreCase))
            {
                return $"{_type.Name}{ES}";
            }
        }

        var postfixExists = _vowels.Select(x => $"{x}{Y}")
            .Any(x => x.ToLower() == _type.Name[^2..]);

        if (postfixExists)
        {
            return $"{_type.Name}{S}";
        }

        if (_type.Name.EndsWith(Y, StringComparison.OrdinalIgnoreCase))
        {
            return $"{_type.Name[..^1]}{I}{ES}";
        }

        return $"{_type.Name}{S}";
    }
}
