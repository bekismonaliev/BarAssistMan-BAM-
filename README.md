# BarAssistMan-BAM-
BarAssistMan(BAM), bar ve restoranlar için stok takibini ve içecek tariflerini yönetmeyi kolaylaştıran bir uygulamadır. Malzemelerin stok seviyelerini kaydetme, kritik seviyeler için uyarı verme, tarif oluşturma ve stok tüketimini izleme gibi işlevler sunar.
BarAssistMan(BAM), bar ve restoranlar için stok takibini ve içecek tariflerini yönetmeyi kolaylaştıran bir uygulamadır. Malzemelerin stok seviyelerini kaydetme, kritik seviyeler için uyarı verme, tarif oluşturma ve stok tüketimini izleme gibi işlevler sunar. Proje Sunumu: Bar Stok ve Tarif Yönetim Sistemi Hazırladığım Bar Stok ve Tarif Yönetim Sistemi projesini tanıtacağım. Proje nesne tabanlı programlama kriterlerine uygun olarak geliştirilmiştir. projede kullandığım sınıfları ve her birinin hangi amaca hizmet ettiğini detaylıca anlatmak istiyorum.

Ingredient (Malzeme) Sınıfı Bu sınıf, barın stoklarında bulunan tüm malzemeleri temsil etmektedir.
Amaç: Malzemelerin isim, miktar, birim ve kritik seviye gibi özelliklerini tanımlamak ve yönetmek. Kapsülleme: Malzeme bilgileri private alanlarda saklanır ve get/set metotları ile erişim sağlanır. Örnek Kullanım: “Şeker” adlı bir malzeme eklendiğinde, miktar ve kritik seviyeleri bu sınıf üzerinden kaydedilir. 2. Recipe (Tarif) Sınıfı Bu sınıf, barın içecek tariflerini tanımlamak için kullanılmıştır.

Amaç: Her tarifin adı, açıklaması ve hangi malzemelerden oluştuğunu belirlemek. Soyutlama: Bir tarifi ve ilgili malzemeleri bir bütün olarak modellememizi sağlar. Polimorfizm: Tarifler dinamik olarak farklı içeriklerle güncellenebilir. Örnek Kullanım: “Mojito” tarifi için kullanılan malzemeler ve miktarlar bu sınıfta saklanır. 3. RecipeIngredient (Tarif-Malzeme Bağı) Sınıfı Tarifler ve malzemeler arasındaki ilişkiyi temsil eden ara bir sınıftır.

Amaç: Bir tarifte hangi malzemenin ne kadar kullanılacağını belirlemek. Kalıtım: Ingredient ve Recipe sınıflarını birbirine bağlar. Örnek Kullanım: Mojito tarifinde “Nane Yaprakları” adlı malzemenin 10 gram kullanıldığını bu sınıf üzerinden takip ederiz. 4. Order (Sipariş) Sınıfı Bu sınıf, bar müşterilerinden alınan siparişleri modellemek için yazılmıştır.

Amaç: Bir siparişin tarihi, içeriği ve ilişkili detayları yönetmek. Encapsulation: Sipariş detayları yalnızca ilgili sınıflar tarafından erişilebilir. Örnek Kullanım: “3 adet Mojito” siparişi alındığında bu sınıf üzerinden kaydedilir. 5. OrderDetail (Sipariş Detayı) Sınıfı Siparişlere ait detayları modellemek için kullanılmıştır.

Amaç: Hangi tariften kaç adet sipariş edildiğini ve bu siparişlerin stoklara etkisini hesaplamak. Kapsülleme: Siparişlerin tariflere olan etkisi, ilgili metotlarla güvenli bir şekilde işlenir. Örnek Kullanım: “Mojito” siparişi için kullanılan tüm malzemelerin stoklardan düşürülmesi. 6. StockManager (Stok Yönetim) Sınıfı Projenin stok yönetim işlevselliğini sağlayan temel sınıftır.

Amaç: Malzemelerin stok miktarlarını yönetmek, kritik seviyeleri kontrol etmek ve yeni stok eklemek. Fonksiyonel Kullanım: Gereksiz kod tekrarından kaçınılmış, esnek metotlarla stok ekleme ve kontrol işlemleri yapılmıştır. Örnek Kullanım: Kritik seviyeyi aşan malzemeler için konsolda otomatik uyarı verir. 7. RecipeManager (Tarif Yönetim) Sınıfı Tariflerin yönetimi ve dinamik olarak eklenmesi veya düzenlenmesi için geliştirilmiştir.

Amaç: Kullanıcıdan tarif alarak kaydetmek veya internetten tarifleri çekmek. Yaratıcılık: OpenAI API entegrasyonu ile internetten tarif alma özelliği geliştirilmiştir. Örnek Kullanım: Kullanıcı “Mojito” yazıp tarif sorguladığında, gerekli malzemeler ve miktarlar otomatik olarak tariflere eklenir. 8. ConsoleMenu (Konsol Menüsü) Sınıfı Tüm sistemi kullanıcı dostu bir şekilde sunmak için geliştirilmiş bir sınıftır.

Amaç: Kullanıcıların malzeme ekleme, stok kontrolü, tarif oluşturma gibi işlemleri kolayca yapmasını sağlamak. Kullanıcı Deneyimi: Basit ve net bir arayüz sunarak kullanıcı hatalarını minimuma indirir. Örnek Kullanım: Kullanıcı menüden bir seçim yaparak malzeme ekleyebilir, tarif oluşturabilir veya sipariş işleyebilir. Projenin Değerlendirme Kriterlerine Uygunluğu Kod Organizasyonu ve Yapısı:

Kod modüler bir şekilde yazılmış ve her sınıf belirli bir işlevi gerçekleştirmek için tasarlanmıştır. Yorum satırlarıyla kodun açıklayıcılığı artırılmıştır. Nesne Tabanlı Programlama Kullanımı:

Soyutlama: Malzemeler, tarifler ve siparişler net bir şekilde soyutlanmıştır. Kapsülleme: Tüm sınıflarda private alanlar ve get/set metotları kullanılmıştır. Kalıtım: Sınıflar arasındaki ilişkiler anlamlı bir şekilde oluşturulmuştur. Polimorfizm: Tariflerin dinamik olarak farklı malzemelerle güncellenmesi sağlanmıştır. Fonksiyonlar ve Şablonlar:

Gereksiz kod tekrarından kaçınılmış ve esnek metotlar oluşturulmuştur. Kütüphane ve Koleksiyon Kullanımı:

.NET standart kütüphanelerinden (System.Collections, System.Linq) etkin şekilde faydalanılmıştır. Projenin Akışı, Özgünlüğü ve İşlevselliği:

Sistem akış diyagramına uygun olarak çalışmakta ve testleri yapılmıştır. OpenAI API ile entegre çalışarak yaratıcı özellikler sunmaktadır. Sunum ve Dökümantasyon:

Kodun açıklaması ve kullanıcı talimatları detaylı bir şekilde sunulmuştur.BarAssistMan(BAM), bar ve restoranlar için stok takibini ve içecek tariflerini yönetmeyi kolaylaştıran bir uygulamadır. Malzemelerin stok seviyelerini kaydetme, kritik seviyeler için uyarı verme, tarif oluşturma ve stok tüketimini izleme gibi işlevler sunar. Proje Sunumu: Bar Stok ve Tarif Yönetim Sistemi Hazırladığım Bar Stok ve Tarif Yönetim Sistemi projesini tanıtacağım. Proje nesne tabanlı programlama kriterlerine uygun olarak geliştirilmiştir. projede kullandığım sınıfları ve her birinin hangi amaca hizmet ettiğini detaylıca anlatmak istiyorum.

Ingredient (Malzeme) Sınıfı Bu sınıf, barın stoklarında bulunan tüm malzemeleri temsil etmektedir.
Amaç: Malzemelerin isim, miktar, birim ve kritik seviye gibi özelliklerini tanımlamak ve yönetmek. Kapsülleme: Malzeme bilgileri private alanlarda saklanır ve get/set metotları ile erişim sağlanır. Örnek Kullanım: “Şeker” adlı bir malzeme eklendiğinde, miktar ve kritik seviyeleri bu sınıf üzerinden kaydedilir. 2. Recipe (Tarif) Sınıfı Bu sınıf, barın içecek tariflerini tanımlamak için kullanılmıştır.

Amaç: Her tarifin adı, açıklaması ve hangi malzemelerden oluştuğunu belirlemek. Soyutlama: Bir tarifi ve ilgili malzemeleri bir bütün olarak modellememizi sağlar. Polimorfizm: Tarifler dinamik olarak farklı içeriklerle güncellenebilir. Örnek Kullanım: “Mojito” tarifi için kullanılan malzemeler ve miktarlar bu sınıfta saklanır. 3. RecipeIngredient (Tarif-Malzeme Bağı) Sınıfı Tarifler ve malzemeler arasındaki ilişkiyi temsil eden ara bir sınıftır.

Amaç: Bir tarifte hangi malzemenin ne kadar kullanılacağını belirlemek. Kalıtım: Ingredient ve Recipe sınıflarını birbirine bağlar. Örnek Kullanım: Mojito tarifinde “Nane Yaprakları” adlı malzemenin 10 gram kullanıldığını bu sınıf üzerinden takip ederiz. 4. Order (Sipariş) Sınıfı Bu sınıf, bar müşterilerinden alınan siparişleri modellemek için yazılmıştır.

Amaç: Bir siparişin tarihi, içeriği ve ilişkili detayları yönetmek. Encapsulation: Sipariş detayları yalnızca ilgili sınıflar tarafından erişilebilir. Örnek Kullanım: “3 adet Mojito” siparişi alındığında bu sınıf üzerinden kaydedilir. 5. OrderDetail (Sipariş Detayı) Sınıfı Siparişlere ait detayları modellemek için kullanılmıştır.

Amaç: Hangi tariften kaç adet sipariş edildiğini ve bu siparişlerin stoklara etkisini hesaplamak. Kapsülleme: Siparişlerin tariflere olan etkisi, ilgili metotlarla güvenli bir şekilde işlenir. Örnek Kullanım: “Mojito” siparişi için kullanılan tüm malzemelerin stoklardan düşürülmesi. 6. StockManager (Stok Yönetim) Sınıfı Projenin stok yönetim işlevselliğini sağlayan temel sınıftır.

Amaç: Malzemelerin stok miktarlarını yönetmek, kritik seviyeleri kontrol etmek ve yeni stok eklemek. Fonksiyonel Kullanım: Gereksiz kod tekrarından kaçınılmış, esnek metotlarla stok ekleme ve kontrol işlemleri yapılmıştır. Örnek Kullanım: Kritik seviyeyi aşan malzemeler için konsolda otomatik uyarı verir. 7. RecipeManager (Tarif Yönetim) Sınıfı Tariflerin yönetimi ve dinamik olarak eklenmesi veya düzenlenmesi için geliştirilmiştir.

Amaç: Kullanıcıdan tarif alarak kaydetmek veya internetten tarifleri çekmek. Yaratıcılık: OpenAI API entegrasyonu ile internetten tarif alma özelliği geliştirilmiştir. Örnek Kullanım: Kullanıcı “Mojito” yazıp tarif sorguladığında, gerekli malzemeler ve miktarlar otomatik olarak tariflere eklenir. 8. ConsoleMenu (Konsol Menüsü) Sınıfı Tüm sistemi kullanıcı dostu bir şekilde sunmak için geliştirilmiş bir sınıftır.

Amaç: Kullanıcıların malzeme ekleme, stok kontrolü, tarif oluşturma gibi işlemleri kolayca yapmasını sağlamak. Kullanıcı Deneyimi: Basit ve net bir arayüz sunarak kullanıcı hatalarını minimuma indirir. Örnek Kullanım: Kullanıcı menüden bir seçim yaparak malzeme ekleyebilir, tarif oluşturabilir veya sipariş işleyebilir. Projenin Değerlendirme Kriterlerine Uygunluğu Kod Organizasyonu ve Yapısı:

Kod modüler bir şekilde yazılmış ve her sınıf belirli bir işlevi gerçekleştirmek için tasarlanmıştır. Yorum satırlarıyla kodun açıklayıcılığı artırılmıştır. Nesne Tabanlı Programlama Kullanımı:

Soyutlama: Malzemeler, tarifler ve siparişler net bir şekilde soyutlanmıştır. Kapsülleme: Tüm sınıflarda private alanlar ve get/set metotları kullanılmıştır. Kalıtım: Sınıflar arasındaki ilişkiler anlamlı bir şekilde oluşturulmuştur. Polimorfizm: Tariflerin dinamik olarak farklı malzemelerle güncellenmesi sağlanmıştır. Fonksiyonlar ve Şablonlar:

Gereksiz kod tekrarından kaçınılmış ve esnek metotlar oluşturulmuştur. Kütüphane ve Koleksiyon Kullanımı:

.NET standart kütüphanelerinden (System.Collections, System.Linq) etkin şekilde faydalanılmıştır. Projenin Akışı, Özgünlüğü ve İşlevselliği:

Sistem akış diyagramına uygun olarak çalışmakta ve testleri yapılmıştır. OpenAI API ile entegre çalışarak yaratıcı özellikler sunmaktadır. Sunum ve Dökümantasyon:

Kodun açıklaması ve kullanıcı talimatları detaylı bir şekilde sunulmuştur.BarAssistMan(BAM), bar ve restoranlar için stok takibini ve içecek tariflerini yönetmeyi kolaylaştıran bir uygulamadır. Malzemelerin stok seviyelerini kaydetme, kritik seviyeler için uyarı verme, tarif oluşturma ve stok tüketimini izleme gibi işlevler sunar. Proje Sunumu: Bar Stok ve Tarif Yönetim Sistemi Hazırladığım Bar Stok ve Tarif Yönetim Sistemi projesini tanıtacağım. Proje nesne tabanlı programlama kriterlerine uygun olarak geliştirilmiştir. projede kullandığım sınıfları ve her birinin hangi amaca hizmet ettiğini detaylıca anlatmak istiyorum.

Ingredient (Malzeme) Sınıfı Bu sınıf, barın stoklarında bulunan tüm malzemeleri temsil etmektedir.
Amaç: Malzemelerin isim, miktar, birim ve kritik seviye gibi özelliklerini tanımlamak ve yönetmek. Kapsülleme: Malzeme bilgileri private alanlarda saklanır ve get/set metotları ile erişim sağlanır. Örnek Kullanım: “Şeker” adlı bir malzeme eklendiğinde, miktar ve kritik seviyeleri bu sınıf üzerinden kaydedilir. 2. Recipe (Tarif) Sınıfı Bu sınıf, barın içecek tariflerini tanımlamak için kullanılmıştır.

Amaç: Her tarifin adı, açıklaması ve hangi malzemelerden oluştuğunu belirlemek. Soyutlama: Bir tarifi ve ilgili malzemeleri bir bütün olarak modellememizi sağlar. Polimorfizm: Tarifler dinamik olarak farklı içeriklerle güncellenebilir. Örnek Kullanım: “Mojito” tarifi için kullanılan malzemeler ve miktarlar bu sınıfta saklanır. 3. RecipeIngredient (Tarif-Malzeme Bağı) Sınıfı Tarifler ve malzemeler arasındaki ilişkiyi temsil eden ara bir sınıftır.

Amaç: Bir tarifte hangi malzemenin ne kadar kullanılacağını belirlemek. Kalıtım: Ingredient ve Recipe sınıflarını birbirine bağlar. Örnek Kullanım: Mojito tarifinde “Nane Yaprakları” adlı malzemenin 10 gram kullanıldığını bu sınıf üzerinden takip ederiz. 4. Order (Sipariş) Sınıfı Bu sınıf, bar müşterilerinden alınan siparişleri modellemek için yazılmıştır.

Amaç: Bir siparişin tarihi, içeriği ve ilişkili detayları yönetmek. Encapsulation: Sipariş detayları yalnızca ilgili sınıflar tarafından erişilebilir. Örnek Kullanım: “3 adet Mojito” siparişi alındığında bu sınıf üzerinden kaydedilir. 5. OrderDetail (Sipariş Detayı) Sınıfı Siparişlere ait detayları modellemek için kullanılmıştır.

Amaç: Hangi tariften kaç adet sipariş edildiğini ve bu siparişlerin stoklara etkisini hesaplamak. Kapsülleme: Siparişlerin tariflere olan etkisi, ilgili metotlarla güvenli bir şekilde işlenir. Örnek Kullanım: “Mojito” siparişi için kullanılan tüm malzemelerin stoklardan düşürülmesi. 6. StockManager (Stok Yönetim) Sınıfı Projenin stok yönetim işlevselliğini sağlayan temel sınıftır.

Amaç: Malzemelerin stok miktarlarını yönetmek, kritik seviyeleri kontrol etmek ve yeni stok eklemek. Fonksiyonel Kullanım: Gereksiz kod tekrarından kaçınılmış, esnek metotlarla stok ekleme ve kontrol işlemleri yapılmıştır. Örnek Kullanım: Kritik seviyeyi aşan malzemeler için konsolda otomatik uyarı verir. 7. RecipeManager (Tarif Yönetim) Sınıfı Tariflerin yönetimi ve dinamik olarak eklenmesi veya düzenlenmesi için geliştirilmiştir.

Amaç: Kullanıcıdan tarif alarak kaydetmek veya internetten tarifleri çekmek. Yaratıcılık: OpenAI API entegrasyonu ile internetten tarif alma özelliği geliştirilmiştir. Örnek Kullanım: Kullanıcı “Mojito” yazıp tarif sorguladığında, gerekli malzemeler ve miktarlar otomatik olarak tariflere eklenir. 8. ConsoleMenu (Konsol Menüsü) Sınıfı Tüm sistemi kullanıcı dostu bir şekilde sunmak için geliştirilmiş bir sınıftır.

Amaç: Kullanıcıların malzeme ekleme, stok kontrolü, tarif oluşturma gibi işlemleri kolayca yapmasını sağlamak. Kullanıcı Deneyimi: Basit ve net bir arayüz sunarak kullanıcı hatalarını minimuma indirir. Örnek Kullanım: Kullanıcı menüden bir seçim yaparak malzeme ekleyebilir, tarif oluşturabilir veya sipariş işleyebilir. Projenin Değerlendirme Kriterlerine Uygunluğu Kod Organizasyonu ve Yapısı:

Kod modüler bir şekilde yazılmış ve her sınıf belirli bir işlevi gerçekleştirmek için tasarlanmıştır. Yorum satırlarıyla kodun açıklayıcılığı artırılmıştır. Nesne Tabanlı Programlama Kullanımı:

Soyutlama: Malzemeler, tarifler ve siparişler net bir şekilde soyutlanmıştır. Kapsülleme: Tüm sınıflarda private alanlar ve get/set metotları kullanılmıştır. Kalıtım: Sınıflar arasındaki ilişkiler anlamlı bir şekilde oluşturulmuştur. Polimorfizm: Tariflerin dinamik olarak farklı malzemelerle güncellenmesi sağlanmıştır. Fonksiyonlar ve Şablonlar:

Gereksiz kod tekrarından kaçınılmış ve esnek metotlar oluşturulmuştur. Kütüphane ve Koleksiyon Kullanımı:

.NET standart kütüphanelerinden (System.Collections, System.Linq) etkin şekilde faydalanılmıştır. Projenin Akışı, Özgünlüğü ve İşlevselliği:

Sistem akış diyagramına uygun olarak çalışmakta ve testleri yapılmıştır. OpenAI API ile entegre çalışarak yaratıcı özellikler sunmaktadır. Sunum ve Dökümantasyon:

Kodun açıklaması ve kullanıcı talimatları detaylı bir şekilde sunulmuştur.
