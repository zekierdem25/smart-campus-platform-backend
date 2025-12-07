import { useState } from "react";
import api from "../api/axios";
import { Link } from "react-router-dom";

export default function Register() {
  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    confirmPassword: "",
  });

  const [message, setMessage] = useState("");

  function handleChange(e) {
    setForm({ ...form, [e.target.name]: e.target.value });
  }

  async function handleSubmit(e) {
    e.preventDefault();

    try {
      const res = await api.post("/auth/register", {
        ...form,
        userType: "Student",
        departmentId: "11111111-1111-1111-1111-111111111111",
        studentNumber: "S" + Math.floor(Math.random() * 999999),
      });

      setMessage(res.data.message);
    } catch (err) {
      setMessage("Registration failed");
    }
  }

  return (
    <div style={{ padding: 40 }}>
      <h2>Register</h2>

      <form onSubmit={handleSubmit} style={{ display: "flex", flexDirection: "column", maxWidth: 300 }}>
        <label>First Name</label>
        <input name="firstName" value={form.firstName} onChange={handleChange} required />

        <label>Last Name</label>
        <input name="lastName" value={form.lastName} onChange={handleChange} required />

        <label>Email</label>
        <input name="email" type="email" value={form.email} onChange={handleChange} required />

        <label>Password</label>
        <input name="password" type="password" value={form.password} onChange={handleChange} required />

        <label>Confirm Password</label>
        <input name="confirmPassword" type="password" value={form.confirmPassword} onChange={handleChange} required />

        <button type="submit" style={{ marginTop: 10 }}>Register</button>
      </form>

      {message && <p>{message}</p>}

      <p>
        Already have an account? <Link to="/login">Login</Link>
      </p>
    </div>
  );
}
