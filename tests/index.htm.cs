<!doctype html>
<html>
  <head>
    <meta charset=utf-8>
    <title>Hesap Hareketleri</title>
    <style type=text/css>
      body,
      html {
        padding: 0 20px;
        margin: 0;
        font-family: Arial, sans-serif;
        font-size: 10pt
      }

      .header .colbar {
        position: relative;
        display: block;
        width: 100%;
        height: 4px;
        line-height: 1px
      }

      .header .Logo {
        margin: 20px 0;
        width: 140px
      }

      .TablePan>table {
        width: 100%;
        border-spacing: 0;
        color: #000;
        border: 1px solid #a1afba;
        border-bottom: 0 solid;
        border-right: 0 solid
      }

      .TablePan>table td {
        background: #fff;
        padding: 6px 8px;
        border-bottom: 1px solid #a1afba;
        border-right: 1px solid #a1afba
      }

      .TablePan table.free {
        width: 100%;
        border-spacing: 0
      }

      .TablePan table.free td {
        padding: 3px 0;
        border-bottom: 0 solid;
        border-right: 0 solid
      }

      .TableList {
        padding: 8px
      }

      .TableList>table {
        width: 100%;
        border-spacing: 0
      }

      .TableList>table td,
      .TableList>table th {
        padding: 8px 10px;
        text-align: left
      }

      .TableList>table td {
        border: 0 solid !important;
        border-top: 1px solid #a1afba !important;
        white-space: nowrap
      }

      .nowrapN {
        white-space: normal !important
      }

      .TableList>table tr:nth-child(even) td {
        background: #eaeff0
      }

      .TList {
        border-bottom: 1px solid #05347a;
        padding-top: 14px
      }

      .TList tbody td,
      .TList tbody th {
        padding: 8px 28px;
        background: #f3f3f3;
        text-align: left;
        color: #000;
        border-left: 2px dotted #e0e3e5;
        border-bottom: 2px dotted #e0e3e5
      }

      .TList tbody td.clr {
        background: #fff
      }

      .TList tbody td:first-child {
        border-left: 0 dotted
      }

      .TList thead th {
        background: #e6e6e6;
        padding: 8px;
        text-align: left;
        color: #05347a
      }

      .TList tbody th {
        background: #edecec;
        text-align: left;
        color: #000
      }

      .TList caption {
        color: #05347a;
        font-size: 15pt;
        border-bottom: 1px solid #05347a;
        text-align: left;
        padding: 6px 20px
      }
    </style>
  </head>
  <body>
    <div class=header>
      <div class=colbar><img src=https://www.burgan.com.tr/assets/images/maildata/colbar.gif width=100% height=4></div><img src=https://www.burgan.com.tr/assets/images/maildata/BurganLogo.gif class=Logo style=left;>
    </div>
    <h2 style=text-align:right;margin-top:30px>SİGORTA BAŞVURU FORMU</h2>
    <div class=TablePan>
      <table>
        <tr>
          <td><b>İşlem No :</b>{{data.FormInstance_Transaction_Id}}</td>
          <td><b>Sigorta Ettiren ile Sigortalı Aynı Kişidir</b><br>{{data.sigortaEttirenIleSigortaliAyniKisidir}}</td>
        </tr>
        <tr>
          <td><b>Sigortalı Adı/Ünvanı :</b>{{data.FormInstance_Approver_Fullname}}</td>
          <td><b>Sigortalı VKN/TCKN :</b>{{data.FormInstance_Approver_CitizenshipNumber}}</td>
        </tr>
        <tr>
          <td><b>Sigorta Ettiren Adı/Ünvanı :</b>{{data.SigortaEttirenAd}}</td>
          <td><b>Sigorta Ettiren VKN/TCKN :</b>{{data.SigortaEttirenVKN}}</td>
        </tr>
        <tr>
          <td colspan=2><b>DAİNİ MÜRTEHİN :</b>{{data.dainiMurtehin}}</td>
        </tr>
      </table>
    </div><br>
    <div class=TList>
      <table cellspacing=0 cellpadding=0 border=0 width=100%>
        <thead>
          <tr>
            <th style=20px;>TALEP EDİLEN SİGORTA TÜRÜ</th>
          </tr>
        </thead>
      </table>
    </div>
    <div class=TablePan>
      <table>
        <tr>
          <td><b>KONUT EŞYA</b><br>{{data.sigortaTuru.esya}}</td>
          <td><b>KONUT</b><br>{{data.sigortaTuru.konut}}</td>
          <td><b>DASK</b><br>{{data.sigortaTuru.dask}}</td>
          <td><b>KASKO</b><br>{{data.sigortaTuru.kasko}}</td>
          <td><b>TRAFİK</b><br>{{data.sigortaTuru.trafik}}</td>
          <td><b>İşyeri</b><br>{{data.sigortaTuru.isyeri}}</td>
          <td><b>DİĞER</b><br>{{data.sigortaTuru.diger}}</td>
        </tr>
      </table>
    </div><br>
    <div class=TList>
      <table cellspacing=0 cellpadding=0 border=0 width=100%>
        <thead>
          <tr>
            <th style=20px;>KONUT-KONUT EŞYA VE DASK İÇİN</th>
          </tr>
        </thead>
      </table>
    </div>
    <div class=TablePan>
      <table>
        <tr>
          <td><b>Adres kodu (UAVT kodu)</b><br>{{data.adresKoduUavtKodu}}</td>
          <td><b>Bina m2</b><br>{{data.binaM2}}</td>
          <td><b>Bina sigorta değeri</b><br>{{data.binaSigortaDegeri}}</td>
          <td><b>Eşya sigorta değeri</b><br>{{data.esyaSigortaDegeri}}</td>
        </tr>
        <tr>
          <td></td>
          <td><b>Bina yapım yılı</b><br>{{data.binaYapimYili}}</td>
          <td><b>Toplam kat adedi</b><br>{{data.toplamKatAdedi}}</td>
          <td><b>Bulunduğu kat</b><br>{{data.bulunduguKat}}</td>
        </tr>
        <tr>
          <td colspan=4><b>Riziko Adresi</b><br>{{data.rizikoAdresi}}</td>
        </tr>
      </table>
      <p style=center><i>{{data.textArea1}}</i></p>
    </div><br>
    <div class=TList>
      <table cellspacing=0 cellpadding=0 border=0 width=100%>
        <thead>
          <tr>
            <th style=20px;>KASKO-TRAFİK İÇİN</th>
          </tr>
        </thead>
      </table>
    </div>
    <div class=TablePan>
      <table>
        <tr>
          <td><b>Plaka</b><br>{{data.plaka}}</td>
          <td><b>Belge seri no</b><br>{{data.belgeSeriNo}}</td>
          <td><b>Araç marka model</b><br>{{data.aracMarkaModel}}</td>
          <td><b>Araç marka kodu</b><br>{{data.aracMarkaKodu}}</td>
          <td><b>Motor / Şase no</b><br>{{data.motorSaseNo}}</td>
        </tr>
        <tr>
          <td colspan=5><b>Sigortalı Adresi</b><br>{{data.sigortaliAdres}}</td>
        </tr>
      </table>
      <p style=center><i>{{data.textArea}}</i></p>
    </div><br>
    <div class=TList>
      <table cellspacing=0 cellpadding=0 border=0 width=100%>
        <thead>
          <tr>
            <th style=20px;>İŞYERİ İÇİN</th>
          </tr>
        </thead>
      </table>
    </div>
    <div class=TablePan>
      <table>
        <tr>
          <td><b>Adres kodu (UAVT kodu)</b><br>{{data.adresKoduUavtKodu1}}</td>
          <td><b>Bina m2</b><br>{{data.binaM3}}</td>
          <td><b>Bina sigorta değeri</b><br>{{data.binaSigortaDegeri1}}</td>
          <td><b>Eşya sigorta değeri</b><br>{{data.esyaSigortaDegeri1}}</td>
        </tr>
        <tr>
          <td></td>
          <td><b>Elektronik cihaz değeri</b><br>{{data.esyaSigortaDegeri2}}</td>
          <td><b>Makine kırılması değeri</b><br>{{data.esyaSigortaDegeri3}}</td>
          <td><b>Demirbaş değeri</b><br>{{data.esyaSigortaDegeri5}}</td>
        </tr>
        <tr>
          <td colspan=5><b>Riziko Adresi</b><br>{{data.rizikoAdresi1}}</td>
        </tr>
      </table>
      <p style=center><i>{{data.textArea2}}</i></p>
    </div><br>
    <div class=TList>
      <table cellspacing=0 cellpadding=0 border=0 width=100%>
        <thead>
          <tr>
            <th style=20px;>DİĞER</th>
          </tr>
        </thead>
      </table>
    </div>
    <div class=TablePan>
      <table>
        <tr>
          <td colspan=5><b>Diğer</b><br>{{data.diger}}</td>
        </tr>
      </table>
      <p style=center><i>{{data.textArea3}}</i></p>
    </div><br>
    <p><b>İşyeri İçin Varsa Ek Bedel ve Bilgiler :</b></p>
    <p>Kredi teminatı olan sigortalarda, sigorta bedeli, sigortanın konusunun piyasa değeri ve değerleme raporundaki minimum sigorta bedelini karşılar. Poliçede dain-i mürtehin Burgan Bank A.Ş. olur. Sigorta ettiren, sigorta şirketini seçmekte serbesttir.</p>
    <p>Yukarıda beyan etmiş olduğum bilgiler çerçevesinde sigorta talebimin alınmasını ve tarafıma sigorta teklifinin yapılmasını talep ederim.</p>
    <div class=TablePan>
      <table>
        <tr>
          <td><b>Sigorta Ettirenin Adı-Soyad</b><br>{{data.SigortaEttirenAd}} {{data.sigortaEttirenSoyad}}</td>
        </tr>
      </table>
    </div>
    <p>Sigorta poliçesi talebiniz kapsamında paylaştığınız kişisel verilerinize ilişkin 6698 sayılı Kişisel Verilerin Korunması Kanunu hakkındaki detaylı bilgilendirmeye www.burgan.com.tr adresinden ulaşabilirsiniz.</p>
  </body>
</html>