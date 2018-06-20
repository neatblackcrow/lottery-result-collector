class ResultEntryPreload extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            countDown: 3
        };

        this.preloadCountDown = this.preloadCountDown.bind(this);
    }

    preloadCountDown() {
        this.setState((prevState, props) => ({
            countDown: prevState.countDown - 1
        }));

        if (this.state.countDown == 0) {
            window.clearInterval(this.timer);

            $.ajax({
                url: "WaitForRewardType",
                method: "POST"
            }).success((data, textStatus, jqXHR) => {

                if (data.status == "ready") {
                    this.props.toggleState();
                }
                else if (data.status == "not-ready") {

                    this.setState({
                        countDown: 3
                    });
                    this.timer = window.setInterval(this.preloadCountDown, 1000);
                }
                else if (data.status == "finished") {

                }

            });
        }
    }

    componentDidMount() {
        this.timer = window.setInterval(this.preloadCountDown, 1000);
    }

    render() {
        return (
            <div className="container">
                <div className="row">
                    <h4>กรุณารอผู้ตรวจสอบการบันทึกผลรางวัลเลือกประเภทรางวัล {this.state.countDown} วินาที</h4>
                </div>
            </div>
            );
    }
}