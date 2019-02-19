import React from 'react';
import AppFooter from '../../components/Footer/Footer';
import './CabinetLayout.css';

import { Menu, Icon, Layout } from 'antd';
import { Link } from 'react-router-dom';
import { currencyCode } from '../../utils/moneyUtils';
import { logOut } from '../../api/api';

// const SubMenu = Menu.SubMenu;
const {
    Content,
    Footer,
    Sider,
} = Layout;

class CabinetLayout extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            collapsed: false
        }
    }

    onCollapse = collapsed => this.setState({ collapsed });

    logOut = () => logOut();

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
                        <Menu.SubMenu key="1" title={
                            <span>
                                <Icon type="credit-card" />
                                <span>Credit cards</span>
                            </span>
                        }
                        >
                            <Menu.Item key="11">
                                <Link to={`/cab/${this.props.role}/credit-cards/list`}>
                                    <span className="nav-text">My credit cards</span>
                                </Link>
                            </Menu.Item>
                            <Menu.Item key="12">
                                <Link to={`/cab/${this.props.role}/credit-cards/order`}>
                                    <span className="nav-text">Order credit cards</span>
                                </Link>
                            </Menu.Item>
                        </Menu.SubMenu>
                        <Menu.Item key="2">
                            <Icon type="bar-chart" />
                            <span className="nav-text">Installment</span>
                        </Menu.Item>
                        <Menu.SubMenu key="3" title={
                            <span className="nav-text">
                                <Icon type="calendar" />
                                <span>Deposit</span>
                            </span>
                        }>
                            <Menu.Item key="31">
                                <Link to={`/cab/${this.props.role}/deposit/${currencyCode.USD}`}>
                                    <span className="nav-text">Dolar</span>
                                </Link>
                            </Menu.Item>
                            <Menu.Item key="32">
                                <Link to={`/cab/${this.props.role}/deposit/${currencyCode.EUR}`}>
                                    <span className="nav-text">Euro</span>
                                </Link>
                            </Menu.Item>
                            <Menu.Item key="33">
                                <Link to={`/cab/${this.props.role}/deposit/${currencyCode.UAH}`}>
                                    <span className="nav-text">Grivna</span>
                                </Link>
                            </Menu.Item>
                            <Menu.Item key="34">
                                <Link to={`/cab/${this.props.role}/deposits`}>
                                    <span className="nav-text">My deposits</span>
                                </Link>
                            </Menu.Item>
                        </Menu.SubMenu>
                        <Menu.Item key="4">
                            <Icon type="appstore" />
                            <span className="nav-text">Other</span>
                        </Menu.Item>
                        <Menu.Item key="5">
                            <Icon type="logout" />
                            <span
                                className="nav-text"
                                onClick={this.logOut}>Log out</span>
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