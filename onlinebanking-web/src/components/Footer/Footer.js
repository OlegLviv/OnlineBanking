import React from 'react';
import './Footer.css';

import { Link } from 'react-router-dom';

export default () => (
    <div className="footer-body">
        <div className="footer-main-links">
            <div>
                <Link to="/">Home</Link>
            </div>
            <div>
                <Link to="/">API</Link>
            </div>
            <div>
                <Link to="/">Contacts</Link>
            </div>
        </div>
        <div className="footer-copyright">
            © 2019 SwiftBank
        </div>
    </div>
);