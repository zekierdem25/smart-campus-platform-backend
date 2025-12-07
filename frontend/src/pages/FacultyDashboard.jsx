// frontend/src/pages/FacultyDashboard.jsx
import { useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import { Link } from "react-router-dom";

export default function FacultyDashboard() {
  const { user } = useContext(AuthContext);

  return (
    <div>
      <h2>Öğretim Üyesi Paneli</h2>
      <p>
        Hoş geldiniz, <strong>{user.firstName} {user.lastName}</strong>. Ders
        yönetimi ve yoklama işlemlerini buradan takip edebilirsiniz.
      </p>

      <div className="dashboard-grid" style={{ marginTop: 24 }}>
        <div className="dashboard-card">
          <div className="dashboard-card-title">Bu Dönem Verdiğiniz Dersler</div>
          <div className="dashboard-card-value">4</div>
          <div className="dashboard-card-sub">Aktif ders sayısı</div>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Bugünkü Dersler</div>
          <div className="dashboard-card-value">2</div>
          <div className="dashboard-card-sub">Günlük ders programınız</div>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Açık Yoklama Oturumları</div>
          <div className="dashboard-card-value">1</div>
          <div className="dashboard-card-sub">
            Devam eden GPS / QR tabanlı yoklama
          </div>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Bekleyen Mazeret Talepleri</div>
          <div className="dashboard-card-value">3</div>
          <div className="dashboard-card-sub">
            Onay bekleyen devamsızlık mazeretleri
          </div>
        </div>
      </div>

      <h3 style={{ marginTop: 32 }}>Hızlı İşlemler</h3>
      <div className="dashboard-grid">
        <div className="dashboard-card">
          <div className="dashboard-card-title">Ders / Sınıf Yönetimi</div>
          <p>Ders içerikleri, sınıf listeleri ve ders planı yönetimi.</p>
          <button className="btn btn-primary" style={{ marginTop: 12 }}>
            Ders yönetimine git
          </button>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Yoklama Oturumu Aç</div>
          <p>
            GPS veya QR kod tabanlı yoklama oturumu oluşturun ve öğrencilerinizin
            derse katılımını takip edin.
          </p>
          <button className="btn btn-primary" style={{ marginTop: 12 }}>
            Yoklama ekranını aç
          </button>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Not / Değerlendirme Girişi</div>
          <p>
            Vize, final ve dönem içi değerlendirmeleri girip harf notu
            hesaplamasını yapın.
          </p>
          <button className="btn btn-primary" style={{ marginTop: 12 }}>
            Not girişine git
          </button>
        </div>

        <div className="dashboard-card">
          <div className="dashboard-card-title">Duyuru Oluştur</div>
          <p>
            Dersleriniz için duyuru paylaşın; öğrencilere bildirim olarak
            gitsin.
          </p>
          <button className="btn btn-primary" style={{ marginTop: 12 }}>
            Duyuru oluştur
          </button>
        </div>
      </div>
    </div>
  );
}