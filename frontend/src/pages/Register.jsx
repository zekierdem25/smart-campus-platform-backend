import React, { useState, useEffect } from 'react';
import { useForm, useWatch } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import { useNavigate, Link } from 'react-router-dom';
import authService from '../services/authService';
import userService from '../services/userService';
import FormInput from '../components/FormInput';
import Alert from '../components/Alert';
import LoadingSpinner from '../components/LoadingSpinner';

const schema = yup.object().shape({
    firstName: yup.string().required('Ad zorunludur').min(2, 'Ad en az 2 karakter olmalıdır'),
    lastName: yup.string().required('Soyad zorunludur').min(2, 'Soyad en az 2 karakter olmalıdır'),
    email: yup.string().email('Geçerli bir email adresi giriniz').required('Email adresi zorunludur'),
    password: yup.string()
        .required('Şifre zorunludur')
        .min(8, 'Şifre en az 8 karakter olmalıdır')
        .matches(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/, 'Şifre en az bir büyük harf, bir küçük harf ve bir rakam içermelidir'),
    confirmPassword: yup.string()
        .oneOf([yup.ref('password'), null], 'Şifreler eşleşmiyor')
        .required('Şifre tekrarı zorunludur'),
    userType: yup.string().required('Kullanıcı tipi seçilmelidir'),
    departmentId: yup.string().required('Bölüm seçimi zorunludur'),
    studentNumber: yup.string().when('userType', {
        is: 'Student',
        then: (schema) => schema.required('Öğrenci numarası zorunludur'),
    }),
    terms: yup.bool().oneOf([true], 'Kullanım koşullarını kabul etmelisiniz'),
});

const Register = () => {
    const [isLoading, setIsLoading] = useState(false);
    const [departments, setDepartments] = useState([]);
    const [apiError, setApiError] = useState(null);
    const navigate = useNavigate();

    const { register, handleSubmit, control, formState: { errors } } = useForm({
        resolver: yupResolver(schema),
        defaultValues: {
            userType: 'Student',
            terms: false
        }
    });

    const userType = useWatch({
        control,
        name: 'userType',
    });

    useEffect(() => {
        const fetchDepartments = async () => {
            try {
                const data = await userService.getDepartments();
                setDepartments(data);
            } catch (error) {
                console.error('Failed to fetch departments', error);
            }
        };
        fetchDepartments();
    }, []);

    const onSubmit = async (data) => {
        setIsLoading(true);
        setApiError(null);
        try {
            await authService.register(data);
            navigate('/verification-pending');
        } catch (error) {
            console.error('Register error:', error);
            if (error.response && error.response.data && error.response.data.message) {
                setApiError(error.response.data.message);
            } else {
                setApiError('Kayıt başarısız. Lütfen bilgilerinizi kontrol ediniz.');
            }
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <div className="min-h-screen flex items-center justify-center bg-gray-50 py-12 px-4 sm:px-6 lg:px-8">
            <div className="max-w-2xl w-full space-y-8 bg-white p-8 rounded-xl shadow-lg">
                <div>
                    <h2 className="mt-6 text-center text-3xl font-extrabold text-gray-900">
                        Kayıt Ol
                    </h2>
                    <p className="mt-2 text-center text-sm text-gray-600">
                        veya <Link to="/login" className="font-medium text-campus-blue hover:text-campus-blue-dark">giriş yap</Link>
                    </p>
                </div>

                <Alert type="error" message={apiError} />

                <form className="mt-8 space-y-6" onSubmit={handleSubmit(onSubmit)}>
                    <div className="grid grid-cols-1 gap-y-6 gap-x-4 sm:grid-cols-6">

                        {/* First Name */}
                        <div className="sm:col-span-3">
                            <FormInput
                                label="Ad"
                                id="firstName"
                                {...register('firstName')}
                                error={errors.firstName}
                            />
                        </div>

                        {/* Last Name */}
                        <div className="sm:col-span-3">
                            <FormInput
                                label="Soyad"
                                id="lastName"
                                {...register('lastName')}
                                error={errors.lastName}
                            />
                        </div>

                        {/* Email */}
                        <div className="sm:col-span-6">
                            <FormInput
                                label="Email Adresi"
                                id="email"
                                type="email"
                                {...register('email')}
                                error={errors.email}
                            />
                        </div>

                        {/* Password */}
                        <div className="sm:col-span-3">
                            <FormInput
                                label="Şifre"
                                id="password"
                                type="password"
                                {...register('password')}
                                error={errors.password}
                            />
                        </div>

                        {/* Confirm Password */}
                        <div className="sm:col-span-3">
                            <FormInput
                                label="Şifre Tekrar"
                                id="confirmPassword"
                                type="password"
                                {...register('confirmPassword')}
                                error={errors.confirmPassword}
                            />
                        </div>

                        {/* User Type */}
                        <div className="sm:col-span-3">
                            <label htmlFor="userType" className="block text-sm font-medium text-gray-700">Kullanıcı Tipi</label>
                            <div className="mt-1">
                                <select
                                    id="userType"
                                    {...register('userType')}
                                    className="shadow-sm focus:ring-campus-green focus:border-campus-green block w-full sm:text-sm border-gray-300 rounded-md p-2 border"
                                >
                                    <option value="Student">Öğrenci</option>
                                    <option value="Faculty">Akademisyen</option>
                                </select>
                                {errors.userType && <p className="text-red-500 text-xs mt-1">{errors.userType.message}</p>}
                            </div>
                        </div>

                        {/* Department */}
                        <div className="sm:col-span-3">
                            <label htmlFor="departmentId" className="block text-sm font-medium text-gray-700">Bölüm</label>
                            <div className="mt-1">
                                <select
                                    id="departmentId"
                                    {...register('departmentId')}
                                    className="shadow-sm focus:ring-campus-green focus:border-campus-green block w-full sm:text-sm border-gray-300 rounded-md p-2 border"
                                >
                                    <option value="">Seçiniz</option>
                                    {departments.map((dept) => (
                                        <option key={dept.id} value={dept.id}>{dept.name}</option>
                                    ))}
                                </select>
                                {errors.departmentId && <p className="text-red-500 text-xs mt-1">{errors.departmentId.message}</p>}
                            </div>
                        </div>

                        {/* Student Number (Conditional) */}
                        {userType === 'Student' && (
                            <div className="sm:col-span-6">
                                <FormInput
                                    label="Öğrenci Numarası"
                                    id="studentNumber"
                                    {...register('studentNumber')}
                                    error={errors.studentNumber}
                                />
                            </div>
                        )}

                        {/* Terms */}
                        <div className="sm:col-span-6">
                            <div className="flex items-start">
                                <div className="flex items-center h-5">
                                    <input
                                        id="terms"
                                        type="checkbox"
                                        {...register('terms')}
                                        className="focus:ring-campus-green h-4 w-4 text-campus-green border-gray-300 rounded"
                                    />
                                </div>
                                <div className="ml-3 text-sm">
                                    <label htmlFor="terms" className="font-medium text-gray-700">
                                        <a href="#" className="text-campus-blue hover:text-campus-blue-dark">Kullanım koşullarını</a> kabul ediyorum.
                                    </label>
                                    {errors.terms && <p className="text-red-500 text-xs mt-1">{errors.terms.message}</p>}
                                </div>
                            </div>
                        </div>

                    </div>

                    <div>
                        <button
                            type="submit"
                            disabled={isLoading}
                            className="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-campus-green hover:bg-campus-green-dark focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-campus-green disabled:opacity-50 disabled:cursor-not-allowed transition-colors duration-200"
                        >
                            {isLoading && <span className="mr-2"><LoadingSpinner size="sm" /></span>}
                            {isLoading ? 'Kayıt Ol' : 'Kayıt Ol'}
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default Register;
