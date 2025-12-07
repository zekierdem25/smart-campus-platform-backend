import React, { forwardRef } from 'react';

const FormInput = forwardRef(({ label, type = 'text', error, ...props }, ref) => {
    return (
        <div>
            {label && (
                <label htmlFor={props.id || props.name} className="block text-sm font-medium text-gray-700">
                    {label}
                </label>
            )}
            <div className="mt-1">
                <input
                    ref={ref}
                    type={type}
                    className={`shadow-sm focus:ring-campus-green focus:border-campus-green block w-full sm:text-sm border-gray-300 rounded-md p-2 border ${error ? 'border-red-500' : ''}`}
                    {...props}
                />
                {error && <p className="text-red-500 text-xs mt-1">{error.message}</p>}
            </div>
        </div>
    );
});

FormInput.displayName = 'FormInput';

export default FormInput;
