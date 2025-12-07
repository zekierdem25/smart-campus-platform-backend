import React from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import Navbar from '../components/Navbar';

const Dashboard = () => {
    const { user } = useAuth();

    if (!user) return null;

    return (
        <div className="min-h-screen bg-gray-100">
            <Navbar />

            <div className="py-10">
                <header>
                    <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
                        <h1 className="text-3xl font-bold text-gray-900">
                            Hoşgeldin, {user.firstName}!
                        </h1>
                    </div>
                </header>
                <main>
                    <div className="max-w-7xl mx-auto sm:px-6 lg:px-8">
                        <div className="px-4 py-8 sm:px-0">
                            {/* Dashboard Content Grid */}
                            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">

                                {/* Common Feature: Profile */}
                                <Link to="/profile" className="block p-6 bg-white rounded-lg shadow hover:shadow-md transition-shadow cursor-pointer border-l-4 border-campus-green">
                                    <h3 className="text-lg font-medium text-gray-900">Profilim</h3>
                                    <p className="mt-2 text-sm text-gray-500">Kişisel bilgilerinizi ve şifrenizi güncelleyin.</p>
                                </Link>

                                {/* Student Specific Features */}
                                {user.role === 'Student' && (
                                    <>
                                        <div className="p-6 bg-white rounded-lg shadow border-l-4 border-blue-500">
                                            <h3 className="text-lg font-medium text-gray-900">Ders Programım</h3>
                                            <p className="mt-2 text-sm text-gray-500">Haftalık ders programınızı görüntüleyin.</p>
                                            <span className="mt-3 inline-block bg-yellow-100 text-yellow-800 text-xs px-2 py-1 rounded">Yakında</span>
                                        </div>
                                        <div className="p-6 bg-white rounded-lg shadow border-l-4 border-blue-500">
                                            <h3 className="text-lg font-medium text-gray-900">Notlarım</h3>
                                            <p className="mt-2 text-sm text-gray-500">Sınav notlarınızı ve transkriptinizi inceleyin.</p>
                                            <span className="mt-3 inline-block bg-yellow-100 text-yellow-800 text-xs px-2 py-1 rounded">Yakında</span>
                                        </div>
                                    </>
                                )}

                                {/* Faculty Specific Features */}
                                {user.role === 'Faculty' && (
                                    <>
                                        <div className="p-6 bg-white rounded-lg shadow border-l-4 border-purple-500">
                                            <h3 className="text-lg font-medium text-gray-900">Derslerim</h3>
                                            <p className="mt-2 text-sm text-gray-500">Verdiğiniz dersleri ve öğrenci listelerini yönetin.</p>
                                            <span className="mt-3 inline-block bg-yellow-100 text-yellow-800 text-xs px-2 py-1 rounded">Yakında</span>
                                        </div>
                                        <div className="p-6 bg-white rounded-lg shadow border-l-4 border-purple-500">
                                            <h3 className="text-lg font-medium text-gray-900">Not Girişi</h3>
                                            <p className="mt-2 text-sm text-gray-500">Sınav ve ödev notlarını sisteme girin.</p>
                                            <span className="mt-3 inline-block bg-yellow-100 text-yellow-800 text-xs px-2 py-1 rounded">Yakında</span>
                                        </div>
                                    </>
                                )}

                                {/* Placeholder for common features */}
                                <div className="p-6 bg-white rounded-lg shadow border-l-4 border-gray-300">
                                    <h3 className="text-lg font-medium text-gray-900">Duyurular</h3>
                                    <p className="mt-2 text-sm text-gray-500">Kampüs duyuruları ve etkinlikler.</p>
                                    <span className="mt-3 inline-block bg-yellow-100 text-yellow-800 text-xs px-2 py-1 rounded">Yakında</span>
                                </div>

                            </div>
                        </div>
                    </div>
                </main>
            </div>
        </div>
    );
};

export default Dashboard;
