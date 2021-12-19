# DynamicConfigApp

Proje iki adet harici Api servis,bir ClassLibrary(Konfigurasyon) ve onun için düzenlenen bir API'dan oluşmaktadır. (ConfLib, ConfLibApiService, ServiceA, ServiceB)
Harici olarak eklenmek istenen API'larda Startup klasörüne aşağıdaki komut dizimini eklemek yeterli olacaktır.
=> services.AddSingleton(s => new ConfigurationService("SERVICEadi", "mongodb://localhost:27017", "Ms cinsinden kontrol etme süresi"));

=> Storage olarak mongodb kullanılmıştır. Oradan okuyamaması vb. durumlarda her bir servis kendi dosyasının içinde son başarılı okuğu konfigurasyon dosyasını kaydeder.
(_appName + "-dynamic-config.json") olacak şekilde buradan da kaydı çekebilir.
