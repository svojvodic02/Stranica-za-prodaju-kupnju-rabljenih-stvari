# vojvodić-sara-1191250193
Projekt za kolegij ASP.NET MVC, ak. god. 2024/25

Tema projekta "SecondHand", stranica za prodaju/kupnju rabljenih stvari. 
Stranica omogućuje registraciju i ulogiravanje korisnika. Omogućuje korisniku dodavanje novih objava i uređivanje istih. 
Korisnik može urediti samo svoje objave ali ne i tuđe. Jedino Admin može urediti sve objave. 
Stranica omogućuje dodavanje slika pomoću biblioteke Dropzone. Te omogućuje prikaz svih objava i detaljan prikaz samo jedne objave.

Smislenost objektnog modela
1. Postoje 4 entity framework klase ( Listing, ListingType, City, Attachment i postoji User klasa)
2. Tipovi podatak u klasama imaju smisla
3. Naznačene su ispravne veze među objektima

MVC Routing i URL prostori 
1. Postoji kompletni izbornik u aplikaciji
2. Custom ruta u RouteConfig-u
3. Ruta definirana atributima/anotacijama

CRUD operacije i osnovni koncepti rukovanja entitetima
1. Moguće je izmjenit barem 2 entiteta(user,listing)
2. Postoji zajednički partial view na edit+create formi
3. Postoji validacija (sever side)
4. Postoje dvije drop down liste (grad i tip objave)
5. Ispravno implementirane migracijske skripte (postoji initial i još jedna migracija)
6. Postoje 3 elementa na sučelju implementirani pomoću Tag Helper-a
7. Postoji datumska kontrola i funkcionira na barem 2 jezika s različitim formatom datuma
8. Korisničko sučelje napravljeno slijeđenjem osnovnih bootstrap principa
9. Postoji "delete" implementiran pomoću AJAX poziva (attachment)

Organizacija aplikacije
1. DAL i model sloj
2. Ispravni elementi u svakom sloju?

Autorizacija i autentikacija 
1. Postoje odvojene role za neke dijelove aplikacije (admin i običan user)

Web API
1. Postoji mogućnost dohvata barem jednog tipa entiteta putem API-ja (lista, preko id-a)
2. Postoji mogućnost dodavanja, izmjene i brisanja barem jednog entiteta putem API-ja