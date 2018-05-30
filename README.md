# Asset Management System
Dokumentacija za projekat iz Razvoja Elektroenergetskog Softvera 

![Slika](https://image.ibb.co/fSNXCd/Capture.png)

1. Lokalni uredjaj
  - Konzolna aplikacija za dodavanje novog lokalnog uredjaja
    Primer unosa novog uredjaja: 
      - ID  -> Jedinstveni ID uredjaja
      - TypeDevice -> 'A' za analogni ili 'D' za digitalni. Dozvoljena su i mala slova 'a' i 'd'
      - Value -> Inicijalna vrednost uredjaja, ON/OFF/Open/Closed za digitalne, ili brojna vrednost za analogne
      - Configuration -> Odabir kontrolera koji ce da vrsi obradu lokalnog uredjaja (XML fajlovi u Communication folderu)
        - treba uneti c1 u konzolu da bi uredjaj bio poslat na prvi kontroler c1.xml
   
   - Ocekivati sadrzaj c1.xml fajla nakon uspesnog dodavanja uredjaja: 
      
            <controller id="1">
              <Device id="1">
                <Type>D</Type>
                <Time>2018-22-05 16:28:00</Time>
                <Value>ON</Value>
              </Device>
            </controller>
            
2. Lokalni kontroler
  - Konzolna aplikacija koja cuva sve promene svih lokalnih uredjaja i salje ih AMS-u nakon odredjenog vremenskog perioda
    - Prilikom startovanja instance lokalnog kontrolera, potrebno je uneti ime (ID) kontrolera i nakon tog momenta on je aktivan i spreman za preuzimanje lokalnih uredjaja. 
    
3. AMS 
  - WPF aplikacija koja cuva sve promene u sistemu i prikazuje ih kroz graficki interfejs. 
  - Izgled AMS.xml fajla (baze) koja cuva informacije o svim promenama
  
        <AMS>
        <LocalControllerCode id="1">
          <Time>2018-27-05 18:23:13</Time>
          <List>
            <Device id="1">
              <Type>a</Type>
              <Time>2018-27-05 18:22:53</Time>
              <Value>180</Value>
              <WorkTime>34</WorkTime>
            </Device>
          </List>
        </LocalControllerCode>
        <LocalControllerCode id="2">
          <Time>2018-27-05 18:23:13</Time>
          <List>
            <Device id="1">
              <Type>D</Type>
              <Time>2018-27-05 18:22:53</Time>
              <Value>ON</Value>
              <WorkTime>100</WorkTime>
            </Device>
          </List>
        </LocalControllerCode>
        </AMS>
