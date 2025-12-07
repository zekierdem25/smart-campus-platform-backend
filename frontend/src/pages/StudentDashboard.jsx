// frontend/src/pages/StudentDashboard.jsx
import { useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import { Link } from "react-router-dom";

export default function StudentDashboard() {
  const { user } = useContext(AuthContext);

  return (
    <div>
      <h2>Öğrenci Paneli</h2>
      <p>
        Hoş geldin, <strong>{user.firstName}</strong>. Aşağıdan kampüs
        işlemlerine hızlıca erişebilirsin.
      </p>

      {/* ÖZET KARTLAR */}
      <div className="dashboard-grid" style={{ marginTop: 24 }}>
        <div className="dashboard-card">
          <div className="dashboard-card-title">Bu Dönem Ders Sayısı</div>
          <div className="dashboard-card-value">6</div>
          <div className="dashboard-card-sub">
            Aktif kayıtlı olduğun dersler
          </div>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Bugünkü Dersler</div>
          <div className="dashboard-card-value">2</div>
          <div className="dashboard-card-sub">
            Ders programına göre bugün
          </div>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Devamsızlık Durumu</div>
          <div className="dashboard-card-value">%15</div>
          <div className="dashboard-card-sub">
            Kritik sınır: %30 (bilgilendirme amaçlı)
          </div>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Cüzdan Bakiyesi</div>
          <div className="dashboard-card-value">₺120</div>
          <div className="dashboard-card-sub">
            Yemekhane ve kampüs içi ödemeler
          </div>
        </div>
      </div>

      {/* HIZLI İŞLEMLER */}
      <h3 style={{ marginTop: 32 }}>Hızlı İşlemler</h3>
      <div className="dashboard-grid">
        <div className="dashboard-card">
          <div className="dashboard-card-title">Ders Kayıt / Kayıt Yenileme</div>
          <p>
            Ders ekleme, bırakma ve danışman onayı işlemlerini bu bölümden
            yapacaksın.
          </p>
          <button className="btn btn-primary" style={{ marginTop: 12 }}>
            Ders kayıt ekranına git
          </button>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Ders Programım</div>
          <p>
            Haftalık ders saatlerini ve dersliklerini tek ekranda
            görüntüleyebilirsin.
          </p>
          <button className="btn btn-primary" style={{ marginTop: 12 }}>
            Ders programını aç
          </button>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Yoklama & GPS / QR</div>
          <p>
            Derse yoklama vermek için GPS tabanlı veya QR kod okutarak giriş
            yapabilirsin.
          </p>
          <button className="btn btn-primary" style={{ marginTop: 12 }}>
            Yoklama ekranına git
          </button>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Yemekhane & Rezervasyon</div>
          <p>
            Günlük yemek menüsünü görüntüle ve yemekhane için rezervasyon
            oluştur.
          </p>
          <button className="btn btn-primary" style={{ marginTop: 12 }}>
            Yemekhane menüsünü gör
          </button>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Etkinlikler & Kulüpler</div>
          <p>
            Kampüs içi etkinlikleri incele, kayıt ol ve kulüp duyurularını takip
            et.
          </p>
          <button className="btn btn-primary" style={{ marginTop: 12 }}>
            Etkinlik listesine git
          </button>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Bildirimlerim</div>
          <p>
            Ders, yoklama, ödeme ve duyurularla ilgili bildirimlerini buradan
            görebilirsin.
          </p>
          <button className="btn btn-primary" style={{ marginTop: 12 }}>
            Bildirim merkezini aç
          </button>
        </div>
      </div>
    </div>
  );
}