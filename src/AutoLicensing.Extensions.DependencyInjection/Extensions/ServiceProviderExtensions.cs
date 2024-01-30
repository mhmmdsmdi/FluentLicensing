﻿using Microsoft.Extensions.DependencyInjection;

namespace AutoLicensing.Extensions.DependencyInjection.Extensions;

public static class ServiceProviderExtensions
{
    public static IServiceCollection AddAutoLicensing(this IServiceCollection services,
        string productName,
        string publicKey,
        string license)
    {
        var signedLicense = License.Verifier
            .WithRsaPublicKey(publicKey)
            .LoadAndVerify(license);

        services.AddSingleton(signedLicense);

        services.AddSingleton<ILicenseProvider>(new LicenseProvider(signedLicense, productName));

        return services;
    }
}