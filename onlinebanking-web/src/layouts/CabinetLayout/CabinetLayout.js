import React from 'react';
import AppFooter from '../../components/Footer/Footer';
import './CabinetLayout.css';

import { Menu, Icon, Layout } from 'antd';
import { Link } from 'react-router-dom';

// const SubMenu = Menu.SubMenu;
const {
    Content, Footer, Sider,
} = Layout;

class CabinetLayout extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            collapsed: false
        }
    }

    onCollapse = collapsed => {
        this.setState({ collapsed });
    }

    render() {
        return (
            <Layout>
                <Sider className="sider-nav"
                    collapsible
                    collapsed={this.state.collapsed}
                    onCollapse={this.onCollapse}>
                    <div className="logo" />
                    <Menu theme="dark"
                        mode="inline"
                        defaultSelectedKeys={['1']}
                    >
                        <Menu.Item key="1">
                            <Link to={`/cab/${this.props.role}/credit-cards`}>
                                <Icon type="credit-card" />
                                <span className="nav-text">Credit cards</span>
                            </Link>
                        </Menu.Item>
                        <Menu.Item key="2">
                            <Icon type="bar-chart" />
                            <span className="nav-text">Installment</span>
                        </Menu.Item>
                        <Menu.Item key="3">
                            <Icon type="calendar" />
                            <span className="nav-text">Deposit</span>
                        </Menu.Item>
                        <Menu.Item key="4">
                            <Icon type="appstore" />
                            <span className="nav-text">Other</span>
                        </Menu.Item>
                    </Menu>
                </Sider>
                <Layout style={{ marginLeft: this.state.collapsed ? 80 : 200 }}>
                    <Content>
                        <div style={{ padding: 24, background: '#fff', textAlign: 'center' }}>
                            {
                                this.props.children
                            }
                        </div>
                    </Content>
                    <Footer>
                        <AppFooter />
                    </Footer>
                </Layout>
            </Layout>
        );
    }
}

export default CabinetLayout;