using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using egitimportali.Models;
using egitimportali.ViewModel;

namespace egitimportali.Controllers
{
    public class ServisController : ApiController
    {
        DB1Entities db = new DB1Entities();
        SonucModel sonuc = new SonucModel();

        #region Ders

        [HttpGet]
        [Route("api/dersliste")]
        public List<DersModel> DersListe()
        {
            List<DersModel> liste = db.Ders.Select(x => new
                DersModel()
            {
                dersId = x.dersId,
                dersAdi = x.dersAdi,
                dersKredi = x.dersKredi,
                dersYeri = x.dersYeri,
                dersKodu = x.dersKodu

            }).ToList();

            return liste;
        }


        [HttpGet]
        [Route("api/dersbyid/{Dersid}")]
        public DersModel DersById(string dersId)
        {
            DersModel kayit = db.Ders.Where(s => s.dersId == dersId).Select(x => new
                DersModel()
            {

                dersId = x.dersId,
                dersAdi = x.dersAdi,
                dersKredi = x.dersKredi,
                dersYeri = x.dersYeri,
                dersKodu = x.dersKodu

            }).SingleOrDefault();
            return kayit;
        }


        [HttpPost]
        [Route("api/dersekle")]
        public SonucModel DersEkle(DersModel model)
        {
            if (db.Ders.Count(s => s.dersKodu == model.dersKodu) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Ders Kodu Kayitlidir!";
                return sonuc;
            }
            Ders yeni = new Ders();
            yeni.dersId = Guid.NewGuid().ToString();
            yeni.dersAdi = model.dersAdi;
            yeni.dersKredi = model.dersKredi;
            yeni.dersYeri = model.dersYeri;
            yeni.dersKodu = model.dersKodu;

            db.Ders.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ders Eklendi";


            return sonuc;
        }


        [HttpPut]
        [Route("api/dersduzenle")]

        public SonucModel DersDuzenle(DersModel model)
        {
            Ders kayit = db.Ders.Where(s => s.dersId == model.dersId).FirstOrDefault();
            if (kayit == null)
            {

                sonuc.islem = false;
                sonuc.mesaj = "Kayit Bulunamadi!";
                return sonuc;

            }

            kayit.dersAdi = model.dersAdi;
            kayit.dersKredi = model.dersKredi;
            kayit.dersYeri = model.dersYeri;
            kayit.dersKodu = model.dersKodu;

            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ders Güncellendi";

            return sonuc;
        }


        [HttpDelete]
        [Route("api/derssil/{dersId}")]

        public SonucModel DersSil(string dersId)
        {
            Ders kayit = db.Ders.Where(s => s.dersId == dersId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }

            if (db.Kayit.Count(s => s.kayitDersId == dersId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Derse Kayıtlı Öğrenci Olduğu İçin Ders Silinemez!";
                return sonuc;
            }

            db.Ders.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ders Silindi";

            return sonuc;
        }


        #endregion


        #region Ogrenci

        [HttpGet]
        [Route("api/ogrenciliste")]
        public List<OgrenciModel> OgrenciListe()
        {
            List<OgrenciModel> liste = db.Ogrenci.Select(x => new
                OgrenciModel()
            {
                ogrId = x.ogrId,
                ogrNo = x.ogrNo,
                ogrAdSoyad = x.ogrAdSoyad,
                ogrMail = x.ogrMail,
                ogrYas = x.ogrYas

            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/ogrencibyid/{Ogrid}")]

        public OgrenciModel OgrenciById(string ogrId)
        {
            OgrenciModel kayit = db.Ogrenci.Where(s => s.ogrId ==
              ogrId).Select(x => new OgrenciModel()
              {

                  ogrId = x.ogrId,
                  ogrNo = x.ogrNo,
                  ogrAdSoyad = x.ogrAdSoyad,
                  ogrMail = x.ogrMail,
                  ogrYas = x.ogrYas

              }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/ogrenciekle")]
        public SonucModel OgrenciEkle(OgrenciModel model)
        {
            if (db.Ogrenci.Count(c => c.ogrNo == model.ogrNo) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Ögrenci Numarasi Kayitlidir!";
                return sonuc;
            }
            Ogrenci yeni = new Ogrenci();
            yeni.ogrId = Guid.NewGuid().ToString();
            yeni.ogrNo = model.ogrNo;
            yeni.ogrAdSoyad = model.ogrAdSoyad;
            yeni.ogrMail = model.ogrMail;
            yeni.ogrYas = model.ogrYas;

            db.Ogrenci.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ögrenci Eklendi";


            return sonuc;
        }

        [HttpPut]
        [Route("api/ogrenciduzenle")]

        public SonucModel OgrenciDuzenle(OgrenciModel model)
        {
            Ogrenci kayit = db.Ogrenci.Where(s => s.ogrId == model.ogrId).FirstOrDefault();
            if (kayit == null)
            {

                sonuc.islem = false;
                sonuc.mesaj = "Kayit Bulunamadi!";
                return sonuc;

            }

            kayit.ogrNo = model.ogrNo;
            kayit.ogrAdSoyad = model.ogrAdSoyad;
            kayit.ogrMail = model.ogrMail;
            kayit.ogrYas = model.ogrYas;

            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "ögrenci Güncellendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/ogrencisil/{ogrId}")]

        public SonucModel OgrenciSil(string ogrId)
        {
            Ogrenci kayit = db.Ogrenci.Where(s => s.ogrId == ogrId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }

            if (db.Kayit.Count(s => s.kayitOgrId == ogrId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Öğrenci Üzerinde Ders Kaydı Olduğu İçin Öğrenci Silinemez!";
                return sonuc;
            }

            db.Ogrenci.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Silindi";

            return sonuc;
        }

        #endregion


        #region Kayit

        [HttpGet]
        [Route("api/ogrencidersliste/{ogrId}")]

        public List<KayitModel> OgrenciDersListe(string ogrId)
        {

            List<KayitModel> liste = db.Kayit.Where(s => s.kayitOgrId == ogrId).Select(x => new
              KayitModel()
            {
                kayitId = x.kayitId,
                kayitDersId = x.kayitDersId,
                kayitOgrId = x.kayitOgrId,

            }).ToList();

            foreach (var kayit in liste)
            {
                kayit.ogrBilgi = OgrenciById(kayit.kayitOgrId);
                kayit.dersBilgi = DersById(kayit.kayitDersId);
            }

            return liste;
        }


        [HttpGet]
        [Route("api/dersogrenciliste/{dersId}")]

        public List<KayitModel> DersOgrenciListe(string dersId)
        {

            List<KayitModel> liste = db.Kayit.Where(s => s.kayitDersId == dersId).Select(x => new
              KayitModel()
            {
                kayitId = x.kayitId,
                kayitDersId = x.kayitDersId,
                kayitOgrId = x.kayitOgrId,

            }).ToList();

            foreach (var kayit in liste)
            {
                kayit.ogrBilgi = OgrenciById(kayit.kayitOgrId);
                kayit.dersBilgi = DersById(kayit.kayitDersId);
            }

            return liste;
        }


        [HttpPost]
        [Route("api/kayitekle")]

        public SonucModel KayitEkle(KayitModel model)
        {
            if (db.Kayit.Count(s => s.kayitDersId == model.kayitDersId && s.kayitOgrId ==
            model.kayitOgrId) > 0)

            {
                sonuc.islem = false;
                sonuc.mesaj = "İlgili Öğrenci Derse Önceden Kayıtlıdır!";
                return sonuc;
            }

            Kayit yeni = new Kayit();
            yeni.kayitId = Guid.NewGuid().ToString();
            yeni.kayitOgrId = model.kayitOgrId;
            yeni.kayitDersId = model.kayitDersId;

            db.Kayit.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ders Kaydı Eklendi";

            return sonuc;
        }


        [HttpDelete]
        [Route("api/kayitsil/{kayitId}")]


        public SonucModel KayitSil(string kayitId)
        {
            Kayit kayit = db.Kayit.Where(s => s.kayitId == kayitId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }

            db.Kayit.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ders Kaydı Silindi";
            return sonuc;

        }

        #endregion


        #region Egitim


        [HttpPost]
        [Route("api/egitimekle/{dersId}")]
        public SonucModel EgitimEkle([FromBody] EgitimModel model, [FromUri] string dersId)
        {
            if (db.Egitim.Any(x => x.egitimAd == model.egitimAd))
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Eğitim Kayıtlıdır!";
                return sonuc;
            }

            Egitim yeni = new Egitim();
            yeni.egitimId = Guid.NewGuid().ToString();
            yeni.egitimAd = model.egitimAd;
            yeni.egitimDersId = dersId;


            db.Egitim.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Eğitim Eklendi";
            return sonuc;
        }

        [HttpGet]
        [Route("api/egitimliste")]
        public List<EgitimModel> EğitimListe()
        {
            List<EgitimModel> liste = db.Egitim.Select(x => new EgitimModel()
            {
                egitimAd = x.egitimAd,
                egitimId = x.egitimId,
                egitimDersId = x.egitimDersId,

            }).ToList();
            return liste;
        }

        [HttpDelete]
        [Route("api/egitimsil/{egitimId}")]
        public SonucModel EgitimSil(string egitimId)
        {
            Egitim kayit = db.Egitim.Where(s => s.egitimId == egitimId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            db.Egitim.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Eğitim Silindi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/egitimduzenle")]
        public SonucModel EgitimDuzenle(EgitimModel model)
        {
            Egitim kayit = db.Egitim.Where(s => s.egitimId == model.egitimId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Egitim Bulunamadı!";
                return sonuc;
            }
            kayit.egitimAd = model.egitimAd;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Eğitim Düzenlendi";
            return sonuc;
        }


        #endregion

    }

}