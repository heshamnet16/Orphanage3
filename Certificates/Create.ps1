﻿& "C:\Program Files (x86)\Windows Kits\8.1\bin\x64\makecert" -n "CN=Orphanage3" -r -pe -a sha512 -cy authority -sv Orphanage3.pvk Orphanage3CA.cer

& "C:\Program Files (x86)\Windows Kits\8.1\bin\x64\pvk2pfx" -pvk "Orphanage3.pvk" -spc "Orphanage3CA.cer" -pfx "Orphanage3CA.pfx" -pi 1111

& "C:\Program Files (x86)\Windows Kits\8.1\bin\x64\makecert" -n "CN=localhost" -iv "Orphanage3.pvk" -ic "Orphanage3CA.cer" -pe -a sha512 -len 4096 -e 01/07/2019 -sky exchange  -eku 1.3.6.1.5.5.7.3.1 -sv Orphanage3L.pvk Orphanage3L.cer

& "C:\Program Files (x86)\Windows Kits\8.1\bin\x64\pvk2pfx" -pvk "Orphanage3L.pvk" -spc "Orphanage3L.cer" -pfx "Orphanage3L.pfx" -pi 2222

netsh http add sslcert ipport=0.0.0.0:1515 certhash=6cf7bc1a58f043d280519c03448678a9933b76a1 appid='{7fb5e937-fae6-4a43-b108-36c0b1143adb}'