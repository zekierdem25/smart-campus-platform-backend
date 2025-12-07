import React from 'react';
import { Link } from 'react-router-dom';

const NotFound = () => {
    return (
        <div className="min-h-screen bg-gray-50 flex flex-col justify-center py-12 sm:px-6 lg:px-8">
            <div className="mt-8 sm:mx-auto sm:w-full sm:max-w-md text-center">
                <h1 className="text-9xl font-bold text-campus-green">404</h1>
                <h2 className="mt-6 text-3xl font-extrabold text-gray-900">
                    Sayfa Bulunamadı
                </h2>
                <p className="mt-2 text-sm text-gray-600">
                    Aradığınız sayfa mevcut değil veya taşınmış olabilir.
                </p>
                <div className="mt-6">
                    <Link
                        to="/dashboard"
                        className="font-medium text-campus-blue hover:text-campus-blue-dark"
                    >
                        Ana Sayfaya Dön
                    </Link>
                </div>
            </div>
        </div>
    );
};

export default NotFound;
