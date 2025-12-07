// frontend/src/pages/AdminDashboard.jsx
import { useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import { Link } from "react-router-dom";

export default function AdminDashboard() {
  const { user } = useContext(AuthContext);

  return (
    <div>
      <h2>Yönetici Paneli</h2>
      <p>
        Hoş geldiniz, <strong>{user.firstName} {user.lastName}</strong>. Kampüs
        genelini buradan yönetebilirsiniz.
      </p>

      <div className="dashboard-grid" style={{ marginTop: 24 }}>
        <div className="dashboard-card">
          <div className="dashboard-card-title">Toplam Öğrenci</div>
          <div className="dashboard-card-value">1240</div>
          <div className="dashboard-card-sub">Sistemde aktif</div>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Toplam Öğretim Üyesi</div>
          <div className="dashboard-card-value">85</div>
          <div className="dashboard-card-sub">Farklı fakültelerden</div>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Aktif Dersler</div>
          <div className="dashboard-card-value">320</div>
          <div className="dashboard-card-sub">Bu dönem açılan dersler</div>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Açık Destek Kayıtları</div>
          <div className="dashboard-card-value">12</div>
          <div className="dashboard-card-sub">İşlem bekleyen talepler</div>
        </div>
      </div>

      <h3 style={{ marginTop: 32 }}>Yönetim İşlemleri</h3>
      <div className="dashboard-grid">
        <div className="dashboard-card">
          <div className="dashboard-card-title">Kullanıcı Yönetimi</div>
          <p>
            Öğrenci, öğretim üyesi ve admin hesaplarını görüntüle, düzenle veya
            pasif hale getir.
          </p>
          <button className="btn btn-primary" style={{ marginTop: 12 }}>
            Kullanıcı listesine git
          </button>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Ders & Program Yönetimi</div>
          <p>
            Fakülte, bölüm, ders ve dönem planlamalarını yönetin; kapasite ve
            kontenjanları güncelleyin.
          </p>
          <button className="btn btn-primary" style={{ marginTop: 12 }}>
            Ders yönetimine git
          </button>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Kampüs Rezervasyonları</div>
          <p>
            Sınıf, laboratuvar, spor salonu ve toplantı odası rezervasyonlarını
            inceleyin ve yönetin.
          </p>
          <button className="btn btn-primary" style={{ marginTop: 12 }}>
            Rezervasyon ekranını aç
          </button>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Finans & Cüzdan İşlemleri</div>
          <p>
            Yemekhane, harç ve kampüs içi ödeme altyapısını yönetin; anlık
            istatistiklere bakın.
          </p>
          <button className="btn btn-primary" style={{ marginTop: 12 }}>
            Finans paneline git
          </button>
        </div>
      </div>
    </div>
  );
}