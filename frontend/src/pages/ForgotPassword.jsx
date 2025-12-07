import React, { useState } from 'react';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import { Link } from 'react-router-dom';
import authService from '../services/authService';
import FormInput from '../components/FormInput';
import Alert from '../components/Alert';
import LoadingSpinner from '../components/LoadingSpinner';

const schema = yup.object().shape({
    email: yup.string().email('Geçerli bir email adresi giriniz').required('Email adresi zorunludur'),
});

const ForgotPassword = () => {
    const [isLoading, setIsLoading] = useState(false);
    const [apiError, setApiError] = useState(null);
    const [successMessage, setSuccessMessage] = useState(null);

    const { register, handleSubmit, formState: { errors }, reset } = useForm({
        resolver: yupResolver(schema),
    });

    const onSubmit = async (data) => {
        setIsLoading(true);
        setApiError(null);
        setSuccessMessage(null);
        try {
            await authService.forgotPassword(data.email);
            setSuccessMessage('Şifre sıfırlama bağlantısı email adresinize gönderildi.');
            reset();
        } catch (error) {
            console.error('Forgot password error:', error);
            if (error.response && error.response.data && error.response.data.message) {
                setApiError(error.response.data.message);
            } else {
                setApiError('İşlem başarısız. Lütfen tekrar deneyiniz.');
            }
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <div className="min-h-screen flex items-center justify-center bg-gray-50 py-12 px-4 sm:px-6 lg:px-8">
            <div className="max-w-md w-full space-y-8 bg-white p-8 rounded-xl shadow-lg">
                <div>
                    <h2 className="mt-6 text-center text-3xl font-extrabold text-gray-900">
                        Şifremi Unuttum
                    </h2>
                    <p className="mt-2 text-center text-sm text-gray-600">
                        Email adresinizi giriniz, size bir sıfırlama bağlantısı gönderelim.
                    </p>
                </div>

                <Alert type="error" message={apiError} />
                <Alert type="success" message={successMessage} />

                <form className="mt-8 space-y-6" onSubmit={handleSubmit(onSubmit)}>
                    <div className="rounded-md shadow-sm -space-y-px">
                        <FormInput
                            id="email-address"
                            label="Email Adresi"
                            type="email"
                            placeholder="Email Adresi"
                            error={errors.email}
                            {...register('email')}
                        />
                    </div>

                    <div>
                        <button
                            type="submit"
                            disabled={isLoading}
                            className="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-campus-green hover:bg-campus-green-dark focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-campus-green disabled:opacity-50 disabled:cursor-not-allowed transition-colors duration-200"
                        >
                            {isLoading && <span className="mr-2"><LoadingSpinner size="sm" /></span>}
                            {isLoading ? 'Gönder' : 'Gönder'}
                        </button>
                    </div>

                    <div className="text-center">
                        <Link to="/login" className="font-medium text-campus-blue hover:text-campus-blue-dark">
                            Giriş Sayfasına Dön
                        </Link>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default ForgotPassword;
