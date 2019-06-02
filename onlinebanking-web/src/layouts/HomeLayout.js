import React from 'react';
import Footer from '../components/Footer/Footer';
import Header from '../components/Header/Header';
import "./HomeLayout.css";

export default ({ children }) => (
    <div className="home-layout">
        <Header />
        {
            children
        }
        <Footer />
    </div>
)