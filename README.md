# Storage interface for Indy SDK

This library provides a wrapper around `indy_register_wallet_storage` API that is not included in the official Indy SDK wrapper for .NET.
The library exposes an interface that can be implemented to provide custom wallet storage. A sample in-memory implementation can be found in the tests project.

## Installation

This package is available from [myget.org](https://www.myget.org/feed/streetcred-id/package/nuget/Streetcred.Indy.Sdk.Storage).

Using package manager

```bash
PM> Install-Package Streetcred.Indy.Sdk.Storage -Source https://www.myget.org/F/streetcred-id/api/v3/index.json
```

Using dotnet cli

```bash
> dotnet add package Streetcred.Indy.Sdk.Storage --source https://www.myget.org/F/streetcred-id/api/v3/index.json 
```

You can also add custom `nuget.config` in your solution folder. See [example here](nuget.config).

## Usage

Implement `IWalletStorage` interface. See the sample implementation of [InMemoryStorage.cs](src/Indy.Sdk.Storage.Tests/InMemoryStorage.cs) for general guidance. Note that the native Indy process runs all wallet access in a single thread, so you don't have to implement concurrency pattern since all calls are guaranteed to be sequential, but it may a good idea to do this anyway, in case anything changes.

Register the custom implementation using:

```cs
await Storage.RegisterWalletStorageAsync("my_storage", storageImplementation)
```

To use this with the Indy SDK, simply pass the storage type name to the `Wallet.CreateAsync` and `Wallet.OpenAsync` methods.

```cs
// Register storage type
var storage = new InMemoryStorage();
await Storage.RegisterWalletStorageAsync("inmem", storage);

// Create and open wallet
var config = JsonConvert.SerializeObject(new {id = "my_wallet", storage_type = "inmem"});
var creds = JsonConvert.SerializeObject(new {key = "secret_key"});

await  Wallet.CreateWalletAsync(config, creds);
var wallet = await Wallet.OpenWalletAsync(config, creds);
```

Please feel free to open an issue or request a feature you think should be included with this library.
