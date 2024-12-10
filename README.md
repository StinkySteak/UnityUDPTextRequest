# UDP Text Transport
a HttpClient/Server-like for unity by utilizing LiteNetLib (UDP)

This plugin is for unity game developer who wants to host a text server.

## Features
- Simple
- Only supports text format (UTF-8)

## Installation
#### Install via git clone
1. Navigate to your project folder
1. Open up Packages folder
1. Clone this package to the Package folder

#### Install via git URL (Package Manager)
```
https://github.com/StinkySteak/UnityUDPTextRequest.git
```
- Create asmdef for LiteNetLib
- Change the metafile guid to `166c61f9e22d9df41a00ea20161d3d57`
- Expected Final result
```
fileFormatVersion: 2
guid: 166c61f9e22d9df41a00ea20161d3d57
AssemblyDefinitionImporter:
  externalObjects: {}
  userData: 
  assetBundleName: 
  assetBundleVariant: 
```

## Dependencies
- [LiteNetLib](https://github.com/RevenantX/LiteNetLib)

## Example Use case
- Retrieving session data from a running game server without connecting to the game
    - Players count
    - Match status

## Usage examples
### Server
```cs
public int ServerPort;
public string Content;
private UDPTextServer _httpServer;

private void Start()
{
    _httpServer = new UDPTextServer(ServerPort);
    _httpServer.SetContent(Content);
    _httpServer.Start();
}

private void Update()
{
    _httpServer.PollUpdate();
}

private void OnDestroy()
{
    _httpServer.Stop();
}
```

### Client
```cs
public int ServerPort;
public string Url;
public string Result;
private UDPTextClient _httpClient;

private IEnumerator Start()
{
    _httpClient = new UDPTextClient(Url, ServerPort);

    _httpClient.SendRequest();

    while (!_httpClient.IsDone)
    {
        _httpClient.PollUpdate();
        yield return null;
    }

    Result = _httpClient.Text;
}
```