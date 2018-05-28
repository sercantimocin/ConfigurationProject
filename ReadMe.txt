Çalıştırmak için yapılması gerekenler

Powershell açılıp
1-) docker build -t webcore:3 -f Dockerfile.web .
2-) docker build -t db:2 -f Dockerfile.database .
3-) docker-compose up
4-) http://localhost:8000 url ine gidilir


Core projesi istenilen dll i oluşturur
Web projesi configurasyon kayıtlarının eklenip,güncellenip,listelendiği uygulama
Data projesi database operasyonları için temel sınıf ve repositoryleri içerir

IntegationTest sisteme entegrasyon testlerini içerir (Core daki dll içim yazıldı testler)
UnitTest çok unit test durumu içermediği için 1 tane örnek yapıp geçtim


