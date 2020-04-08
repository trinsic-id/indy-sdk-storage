using System;
using System.Linq;
using System.Threading.Tasks;
using Hyperledger.Indy.DidApi;
using Hyperledger.Indy.WalletApi;
using Indy.Sdk.Storage.Tests;
using Newtonsoft.Json;
using Xunit;

namespace Streetcred.Indy.Sdk.Tests
{
    public class InMemoryStorageTests
    {
        [Fact]
        public async Task CanRegisterCustomStorage()
        {
            var storage = new InMemoryStorage();

            await Storage.RegisterWalletStorageAsync("inmem", storage);
        }

        [Fact]
        public async Task ThrowsIfRegisterCalledTwice()
        {
            await Storage.RegisterWalletStorageAsync("inmem1", new InMemoryStorage());

            await Assert.ThrowsAsync<StorageException>(async () =>
                await Storage.RegisterWalletStorageAsync("inmem1", new InMemoryStorage()));
        }

        [Fact]
        public async Task ThrowsIfNullStorage()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await Storage.RegisterWalletStorageAsync("inmem", null));
        }

        [Fact]
        public async Task ThrowsIfNullStorageType()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await Storage.RegisterWalletStorageAsync(null, new InMemoryStorage()));
        }

        [Fact]
        public async Task AddRecordsToStorage()
        {
            const string storageType = "inmem2"; // different type name to avoid throw

            // Register storage type
            var storage = new InMemoryStorage();
            await Storage.RegisterWalletStorageAsync(storageType, storage);

            // Create and open wallet
            var config = JsonConvert.SerializeObject(new {id = "my_wallet", storage_type = storageType});
            var creds = JsonConvert.SerializeObject(new {key = "secret_key"});

            await  Wallet.CreateWalletAsync(config, creds);
            var wallet = await Wallet.OpenWalletAsync(config, creds);

            // Create new did in wallet and test for two records written
            var myDid = await Did.CreateAndStoreMyDidAsync(wallet, "{}");
            Assert.Equal(2, storage.StoredRecords.Count);

            // Retireve key and compare to generated key
            var myKey = await Did.KeyForLocalDidAsync(wallet, myDid.Did);
            Assert.Equal(myKey, myDid.VerKey);

            // Cleanup
            await wallet.CloseAsync();
            await Wallet.DeleteWalletAsync(config, creds);
        }
    }
}
