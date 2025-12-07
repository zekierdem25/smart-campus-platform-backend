import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import authService from '../services/authService';

const VerifyEmail = () => {
    const { token } = useParams();
    const navigate = useNavigate();
    const [status, setStatus] = useState('verifying'); // verifying, success, error
    const [message, setMessage] = useState('Email adresiniz doğrulanıyor, lütfen bekleyiniz...');

    useEffect(() => {
        const verify = async () => {
            try {
                await authService.verifyEmail(token);
                setStatus('success');
                setMessage('Email adresiniz başarıyla doğrulandı! Giriş sayfasına yönlendiriliyorsunuz...');
                setTimeout(() => {
                    navigate('/login');
                }, 3000);
            } catch (error) {
                setStatus('error');
                if (error.response && error.response.data && error.response.data.message) {
                    setMessage(error.response.data.message);
                } else {
                    setMessage('Doğrulama işlemi başarısız oldu. Token geçersiz veya süresi dolmuş olabilir.');
                }
            }
        };

        if (token) {
            verify();
        } else {
            setStatus('error');
            setMessage('Geçersiz doğrulama bağlantısı.');
        }
    }, [token, navigate]);

    return (
        <div className="min-h-screen flex items-center justify-center bg-gray-50 py-12 px-4 sm:px-6 lg:px-8">
            <div className="max-w-md w-full space-y-8 bg-white p-8 rounded-xl shadow-lg text-center">
                <div>
                    <h2 className="mt-6 text-3xl font-extrabold text-gray-900">
                        Email Doğrulama
                    </h2>
                </div>

                <div className={`rounded-md p-4 ${status === 'success' ? 'bg-green-50 text-green-700' :
                        status === 'error' ? 'bg-red-50 text-red-700' : 'bg-blue-50 text-blue-700'
                    }`}>
                    <div className="flex justify-center mb-4">
                        {status === 'verifying' && (
                            <svg className="animate-spin h-8 w-8 text-campus-blue" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                                <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4"></circle>
                                <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
                            </svg>
                        )}
                        {status === 'success' && (
                            <svg className="h-8 w-8 text-green-500" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" />
                            </svg>
                        )}
                        {status === 'error' && (
                            <svg className="h-8 w-8 text-red-500" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
                            </svg>
                        )}
                    </div>
                    <p className="text-sm font-medium">
                        {message}
                    </p>
                </div>

                {status === 'error' && (
                    <div className="mt-4">
                        <button
                            onClick={() => navigate('/login')}
                            className="text-campus-blue hover:text-campus-blue-dark font-medium"
                        >
                            Giriş Sayfasına Dön
                        </button>
                    </div>
                )}
            </div>
        </div>
    );
};

export default VerifyEmail;
