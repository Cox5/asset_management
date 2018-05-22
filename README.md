# asset_management
Dokumentacija za projekat iz Razvoja Elektroenergetskog Softvera 

1. Lokalni uredjaj
  Konzolna aplikacija za dodavanje novog lokalnog uredjaja
    Primer unosa novog uredjaja: 
      ID  -> String
      TypeDevice -> 'A' za analogni ili 'D' za digitalni. Dozvoljena su i mala slova 'a' i 'd'
      Value -> Inicijalna vrednost uredjaja, ON/OFF/Open/Closed za digitalne, ili brojna vrednost za analogne
      Configuration -> Odabir kontrolera koji ce da vrsi obradu lokalnog uredjaja (XML fajlovi u Communication folderu)
        - treba uneti c1 u konzolu da bi uredjaj bio poslat na prvi kontroler c1.xml
   
   Ocekivati sadrzaj c1.xml fajla nakon uspesnog dodavanja uredjaja: 
      
            <controller id="1">
              <Device id="1">
                <Type>D</Type>
                <Time>2018-22-05 16:28:00</Time>
                <Value>ON</Value>
              </Device>
            </controller>
