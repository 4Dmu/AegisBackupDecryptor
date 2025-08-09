using System.Text.Json.Serialization;

public partial class Backup
{
    [JsonPropertyName("version")]
    public long Version { get; set; }

    [JsonPropertyName("header")]
    required public Header Header { get; set; }

    [JsonPropertyName("db")]
    required public string Db { get; set; }
}

public partial class Header
{
    [JsonPropertyName("slots")]
    required public Slot[] Slots { get; set; }

    [JsonPropertyName("params")]
    required public Params Params { get; set; }
}

public partial class Params
{
    [JsonPropertyName("nonce")]
    required public string Nonce { get; set; }

    [JsonPropertyName("tag")]
    required public string Tag { get; set; }
}

public partial class Slot
{
    [JsonPropertyName("type")]
    public long Type { get; set; }

    [JsonPropertyName("uuid")]
    public Guid Uuid { get; set; }

    [JsonPropertyName("key")]
    required public string Key { get; set; }

    [JsonPropertyName("key_params")]
    required public Params KeyParams { get; set; }

    [JsonPropertyName("n")]
    public int N { get; set; }

    [JsonPropertyName("r")]
    public int R { get; set; }

    [JsonPropertyName("p")]
    public int P { get; set; }

    [JsonPropertyName("salt")]
    required public string Salt { get; set; }

    [JsonPropertyName("repaired")]
    public bool Repaired { get; set; }

    [JsonPropertyName("is_backup")]
    public bool IsBackup { get; set; }
}