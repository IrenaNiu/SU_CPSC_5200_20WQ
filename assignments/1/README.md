# CPSC-5200-01 Software Architecture and Design Assignment #1

For the assignment that's due next Thursday. Please clone the class github repro into your personal github, work on your code there, and issue a pull request back to me. Other options are to simply copy the code into a public repo somewhere and send me a link. Packing things as a ZIP doesn't work because the SU email system is configured to eat ZIP files.

You will need to [install dotnet core](https://dotnet.microsoft.com/download) to make this assignment work.

- Clone the `restapi` repository
- Make sure you can build (`dotnet build`)
- Make sure you can run the starting project (`dotnet run` + some `curl` commands)

This is the assignment

- Add support (state management, semantics, etc.) for:
  - Remove (DELETE) a draft or cancelled timecard
  - Replace (POST) a complete line item
  - Update (PATCH) a line item
  - Verify that timecard person is consistent throughout the timecard's lifetime
  - Verify that timecard approver is not timecard person
  - Add support to root document for creating a timesheet

And this is cool stuff you might think about trying

- Complete the state transitions
  - There's no way to transition _back_ to the draft state
- See if you can add a new repository to the database to record People
  - Then expose a read-only API on top of it
  - Use the new repository to validate the people who interact with the timecards

If you want to swap the code over to use 'airline-style' strings for the timecard and line identity, you can use one of the following functions. Note, this just makes the URLs look better and reduces the payload size. Sending a GUID around is kinda ugly if you ask me.

```csharp
static string HashString(string plaintext, int length)
{
    var result = new char[length];

    using (var hasher = new SHA1Managed())
    {
        var bytes = Encoding.UTF8.GetBytes(plaintext);
        var hash = hasher.ComputeHash(bytes);

        for (var i = 0; i < result.Length; i++)
        {
            result[i] = chars[hash[i] % chars.Length];
        }
    }

    return new string(result);
}

static string GetUniqueKey(int size)
{
    var data = new byte[4 * size];
    using (var crypto = new RNGCryptoServiceProvider())
    {
        crypto.GetBytes(data);
    }

    var result = new char[size];
    for (var i = 0; i < size; i++)
    {
        var rnd = BitConverter.ToUInt32(data, i * 4);
        var idx = rnd % chars.Length;

        result[i] = chars[idx];
    }

    return new string(result);
}
```
