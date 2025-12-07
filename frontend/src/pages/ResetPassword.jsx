import React, { useState } from 'react';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import { useNavigate, useParams, Link } from 'react-router-dom';
import authService from '../services/authService';
import FormInput from '../components/FormInput';
import Alert from '../components/Alert';
import LoadingSpinner from '../components/LoadingSpinner';

const schema = yup.object().shape({
    password: yup.string()
        .required('Yeni şifre zorunludur')
        .min(8, 'Şifre en az 8 karakter olmalıdır')
        .matches(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/, 'Şifre en az bir büyük harf, bir küçük harf ve bir rakam içermelidir'),
    confirmPassword: yup.string()
        .oneOf([yup.ref('password'), null], 'Şifreler eşleşmiyor')
        .required('Şifre tekrarı zorunludur'),
});

const ResetPassword = () => {
    const [isLoading, setIsLoading] = useState(false);
    const [apiError, setApiError] = useState(null);
    const [successMessage, setSuccessMessage] = useState(null);
    const navigate = useNavigate();
    const { token } = useParams();

    const { register, handleSubmit, formState: { errors } } = useForm({
        resolver: yupResolver(schema),
    });

    const onSubmit = async (data) => {
        setIsLoading(true);
        setApiError(null);
        setSuccessMessage(null);
        try {
            await authService.resetPassword({
                token,
                password: data.password,
                confirmPassword: data.confirmPassword
            });

            setSuccessMessage('Şifreniz başarıyla sıfırlandı. Giriş sayfasına yönlendiriliyorsunuz...');
            setTimeout(() => {
                navigate('/login');
            }, 3000);
        } catch (error) {
            console.error('Reset password error:', error);
            if (error.response && error.response.data && error.response.data.message) {
                setApiError(error.response.data.message);
            } else {
                setApiError('Şifre sıfırlama işlemi başarısız. Linkin süresi dolmuş olabilir.');
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
                        Yeni Şifre Belirle
                    </h2>
                    <p className="mt-2 text-center text-sm text-gray-600">
                        Lütfen yeni şifrenizi giriniz.
                    </p>
                </div>

                <Alert type="error" message={apiError} />
                <Alert type="success" message={successMessage} />

                {!successMessage && (
                    <form className="mt-8 space-y-6" onSubmit={handleSubmit(onSubmit)}>
                        <div className="rounded-md shadow-sm -space-y-px">
                            <FormInput
                                id="password"
                                label="Yeni Şifre"
                                type="password"
                                placeholder="Yeni Şifre"
                                error={errors.password}
                                {...register('password')}
                            />
                            <div className="mt-4">
                                <FormInput
                                    id="confirmPassword"
                                    label="Şifre Tekrar"
                                    type="password"
                                    placeholder="Şifre Tekrar"
                                    error={errors.confirmPassword}
                                    {...register('confirmPassword')}
                                />
                            </div>
                        </div>

                        <div>
                            <button
                                type="submit"
                                disabled={isLoading}
                                className="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-campus-green hover:bg-campus-green-dark focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-campus-green disabled:opacity-50 disabled:cursor-not-allowed transition-colors duration-200"
                            >
                                {isLoading && <span className="mr-2"><LoadingSpinner size="sm" /></span>}
                                {isLoading ? 'Şifreyi Sıfırla' : 'Şifreyi Sıfırla'}
                            </button>
                        </div>
                    </form>
                )}

                <div className="text-center mt-4">
                    <Link to="/login" className="font-medium text-campus-blue hover:text-campus-blue-dark">
                        Giriş Sayfasına Dön
                    </Link>
                </div>
            </div>
        </div>
    );
};

export default ResetPassword;
