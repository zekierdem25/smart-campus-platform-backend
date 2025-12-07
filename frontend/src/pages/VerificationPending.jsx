import React from 'react';
import { Link } from 'react-router-dom';

const VerificationPending = () => {
    return (
        <div className="min-h-screen flex items-center justify-center bg-gray-50 py-12 px-4 sm:px-6 lg:px-8">
            <div className="max-w-md w-full space-y-8 bg-white p-8 rounded-xl shadow-lg text-center">
                <div>
                    <div className="mx-auto flex items-center justify-center h-12 w-12 rounded-full bg-green-100 mb-4">
                        <svg className="h-6 w-6 text-green-600" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M3 8l7.89 5.26a2 2 0 002.22 0L21 8M5 19h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v10a2 2 0 002 2z" />
                        </svg>
                    </div>
                    <h2 className="text-3xl font-extrabold text-gray-900 mb-2">
                        Emailinizi Kontrol Edin
                    </h2>
                    <p className="text-gray-600 mb-8">
                        Kayıt işleminizi tamamlamak için email adresinize bir doğrulama bağlantısı gönderdik. Lütfen gelen kutunuzu kontrol edin.
                    </p>
                    <div className="text-sm">
                        <Link to="/login" className="font-medium text-campus-blue hover:text-campus-blue-dark">
                            Giriş sayfasına dön
                        </Link>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default VerificationPending;
